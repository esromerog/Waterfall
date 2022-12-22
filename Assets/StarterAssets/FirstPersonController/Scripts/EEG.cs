using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;

using brainflow;
using brainflow.math;

public class EEG : MonoBehaviour
{
	#region private members 	
	private TcpClient socketConnection;
	private Thread clientReceiveThread;
	#endregion
	// Use this for initialization 	
	private BoardShim board_shim = null;
	private int sampling_rate = 0;
	private int[] eeg_channels;
	public float value;
    private Thread calibrationThread;
    private bool calibrado;
    private double calibrationData;
    private int calibrationFrames;

    private double baseBeta;

    private int concent_send;
    private double totalConcentration;

	void Start()
	{
		try
        {
            BrainFlowInputParams input_params = new BrainFlowInputParams();
            int board_id = 37;
            input_params.mac_address="F4:0E:11:75:8D:9F";
            board_shim = new BoardShim(board_id, input_params);
        	eeg_channels = BoardShim.get_eeg_channels(board_id);
            board_shim.prepare_session();
            board_shim.start_stream(3600);
            sampling_rate = BoardShim.get_sampling_rate(board_id);
            Debug.Log("Brainflow streaming was started");
        }
        catch (BrainFlowError e)
        {
            Debug.Log(e);
        }
        value=0;
	}


	// Update is called once per frame
	void Update()
	{
        if (board_shim==null){
            return;
        }
        if (Time.timeSinceLevelLoad<10){
            calibrationData+=BetaPower();
            calibrationFrames++;
        } else {
            if(!calibrado) {
                baseBeta=calibrationData/calibrationFrames;
                calibrado=true;
            }
        }
        if (calibrado) {
            double currentBeta=BetaPower();
            double concentracion=100*(currentBeta-baseBeta)/baseBeta;
            totalConcentration+=concentracion;
            concent_send++;
            if (concent_send==3){
                Debug.Log(value);
                value=(float)Remap(totalConcentration/concent_send,-50,50,0,1);
                concent_send=0;
                totalConcentration=0;
            }
        }
	}

    private void OnDestroy()
    {
        if (board_shim != null)
        {
            try
            {
                board_shim.release_session();
            }
            catch (BrainFlowError e)
            {
                Debug.Log(e);
            }
            Debug.Log("Brainflow streaming was stopped");
        }
    }
    double BetaPower() {
        int nfft = DataFilter.get_nearest_power_of_two (sampling_rate);
        double[,] data = board_shim.get_current_board_data(nfft);
        // use second channel of synthetic board to see 'alpha'
        int channel = eeg_channels[1];
        double[] detrend = DataFilter.detrend (data.GetRow (channel), (int)DetrendOperations.LINEAR);
        Tuple<double[], double[]> psd = DataFilter.get_psd_welch (detrend, nfft, nfft / 2, sampling_rate, (int)WindowOperations.HANNING);
        double band_power_beta = DataFilter.get_band_power (psd, 14.0, 30.0);
        double total_power=DataFilter.get_band_power(psd,0,60);
        //value=(float)band_power_beta;
        return band_power_beta/total_power;
    }
    private double Remap (double value, double from1, double to1, double from2, double to2) {
        if (value>to1) value=to1;
        if (value<from1) value=from1;
        return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
    }

}
