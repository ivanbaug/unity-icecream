using UnityEngine;
using System.Collections;
using System.IO.Ports;

public class WorldGUI : MonoBehaviour {

    //Initialize Serial Port
    SerialPort sPort = new SerialPort("COM6", 115200, Parity.None, 8, StopBits.One); //StopBits Important Stuff

    //Textures (Images for GUI)
    public Texture2D mOnBox;
    public Texture2D mOffBox;

    private string showOnGUI = "";
    private byte[] byTX = { 0, 0, 0, 0, 0 };
    private string strSend;

    //So many states:
    #region State of each pin

    //PresentState
    public static bool sI53 = false;
    public static bool sO52 = false;
    public static bool sI51 = false;
    public static bool sO50 = false;
    public static bool sI49 = false;
    public static bool sO48 = false;
    public static bool sI47 = false;
    public static bool sO46 = false;
    public static bool sI45 = false;
    public static bool sO44 = false;
    public static bool sI43 = false;
    public static bool sO42 = false;
    public static bool sI41 = false;
    public static bool sO40 = false;
    public static bool sI39 = false;
    public static bool sO38 = false;
    public static bool sI37 = false;
    public static bool sO36 = false;
    public static bool sI35 = false;
    public static bool sO34 = false;

    //LastStates
    private static bool lsI53 = false;
    private static bool lsO52 = false;
    private static bool lsI51 = false;
    private static bool lsO50 = false;
    private static bool lsI49 = false;
    private static bool lsO48 = false;
    private static bool lsI47 = false;
    private static bool lsO46 = false;
    private static bool lsI45 = false;
    private static bool lsO44 = false;
    private static bool lsI43 = false;
    private static bool lsO42 = false;
    private static bool lsI41 = false;
    private static bool lsO40 = false;
    private static bool lsI39 = false;
    private static bool lsO38 = false;
    private static bool lsI37 = false;
    private static bool lsO36 = false;
    private static bool lsI35 = false;
    private static bool lsO34 = false;

    #endregion

    /*sendDigital(int ip, char s) this function is usually called when
    // a digital input has changed its state, to send the information 
    // to The arduino over serial port.
    // arguments:
    // int op : Stands for output port... that number will be sent to
    // identify which pin has changed.
    // char s : The state, 0 or 1.
    */
    void sendDigital(int op, char s)
    {
        //strSend = "s";
        byTX[0] = (byte)'s';
        byTX[1] = (byte)op;
        byTX[2] = (byte)s;
        byTX[3] = (byte)10;
        byTX[4] = (byte)255;
        //sPort.Write(strSend);
        sPort.Write(byTX, 0, 4); //Read and remember documentation on this
        //new WaitForSeconds(0.1f);
        //sPort.Write(byTX, 0, 4);
    }

    IEnumerator TimerEnumerator() //Timer that waits Big Silo to be ready
    {
        yield return new WaitForSeconds(3);
        sO50 = true;
    }

	// Use this for initialization
	void Start () {
        //Initialize SerialPort
        if (sPort != null)
        {
            if (!sPort.IsOpen)
            {
                sPort.Open();
                sPort.ReadTimeout = 1;
            }
            else
            {
                sPort.Close();
            }
        }
	    
	}
	
	// Update is called once per frame 
	void Update () {

        if (sPort.IsOpen)
        {
            try
            {
                string recived = sPort.ReadLine(); //Read the information
                if (recived[0] == 's')
                {
                    //if (sI35) { sendDigital(50, '1'); } else { sendDigital(50, '0'); }
                    switch ((int)recived[1])
                    {
                        case 53:
                            sI53 = (recived[2] == '1');                           
                            break;
                        case 51:
                            sI51 = (recived[2] == '1');
                            break;
                        case 49:
                            sI49 = (recived[2] == '1');                            
                            break;
                        case 47:
                            sI47 = (recived[2] == '1');
                            break;
                        case 45:
                            sI45 = (recived[2] == '1');
                            break;
                        case 43:
                            sI43 = (recived[2] == '1');
                            break;
                        case 41:
                            sI41 = (recived[2] == '1');
                            break;
                        case 39:
                            sI39 = (recived[2] == '1');
                            break;
                        case 37:
                            sI37 = (recived[2] == '1');
                            break;
                        case 35:
                            sI35 = (recived[2] == '1');
                            break;

                        default:
                            // Code
                            break;
                    }
                    //sPort.BaseStream.Flush();
                    showOnGUI = recived;
                    
                }
                //recibe analogo
                else if (recived[0]=='a')
                {

                }
            }
            catch (System.Exception) { }
        }


    //Horrible if statements
        //Inputs
        if (lsI35 != sI35) { lsI35 = sI35; }
        if (lsI37 != sI37) { lsI37 = sI37; }
        if (lsI39 != sI39) { lsI39 = sI39; }
        if (lsI41 != sI41) { lsI41 = sI41; }
        if (lsI43 != sI43) { lsI43 = sI43; }
        if (lsI45 != sI45) 
        {
            if (sI45) { CupGenerator.moveCupCommand=true; 
            } 
            lsI45 = sI45; 
        }

        if (lsI47 != sI47) 
        {
            if (sI47) { //CupGenerator.addCupCommand=true; 
            }
            lsI47 = sI47;
        }
        if (lsI49 != sI49) { lsI49 = sI49; }
        if (lsI51 != sI51) { lsI51 = sI51; }
        if (lsI53 != sI53) { lsI53 = sI53; }

        //Outputs
        if (lsO34 != sO34) { lsO34 = sO34; }
        if (lsO36 != sO36) { lsO36 = sO36; }

        if (lsO38 != sO38)//Sensor Lid
        {
            if (sO38) { sendDigital(38, '1'); }
            else { sendDigital(38, '0'); }
            lsO38 = sO38; 
        }
        if (lsO40 != sO40) //Sensor chocolate is full
        {
            if (sO40) { sendDigital(40, '1'); }
            else { sendDigital(40, '0'); }
            lsO40 = sO40; 
        }

        if (lsO42 != sO42)  //Sensor sabor 2 llego
        {
            if (sO42) { sendDigital(42, '1'); }
            else { sendDigital(42, '0'); }
            lsO42 = sO42; 
        }

        if (lsO44 != sO44) //Sensor vanilla is full 
        {
            if (sO44) { sendDigital(44, '1'); }
            else { sendDigital(44, '0'); }
            lsO44 = sO44; 
        }
        if (lsO46 != sO46) //Sensor sabor 1 llego
        {
            if (sO46) { sendDigital(46, '1'); }
            else { sendDigital(46, '0'); }
            lsO46 = sO46; 
        }
        
        if (lsO48 != sO48) //Sensor the cup is on the Conveyor Belt
        {
            if (sO48) { sendDigital(48, '1'); }
            else { sendDigital(48, '0'); }
            lsO48 = sO48; 
        }
        if (lsO50 != sO50) //Big Silo is ready AKA mezcla lista
        {
            if (sO50) { sendDigital(50, '1'); }
            else { sendDigital(50, '0'); }
            lsO50 = sO50; }
        if (lsO52 != sO52) //Start Button
        { 
            if (sO52) { sendDigital(52, '1');}
            else { sendDigital(52, '0');}
            StartCoroutine(TimerEnumerator()); //Starts Timer to wait for Big Silo to be ready
            lsO52 = sO52;
        }
	
	}

    void OnGUI()
    {
        int groupWidth = 560;
        int groupHeight = 70;

        int g1OriginX = 5; //Group1 Origin X
        int g1OriginY = Screen.height - 150; //Group1 Origin Y

        int g2OriginX = 5; //Group2 Origin X
        int g2OriginY = Screen.height - 75; //Group2 Origin Y

        int spacingPictureX = 10;
        int spacingPictureY = 12;

        int spacingLabel1X = 10;
        int spacingLabel1Y = 0;

        int spacingLabel2X = 3;
        int spacingLabel2Y = 30;

        int spacingSubGroup = 47;

        int n = 1;

        GUIStyle labelStyle1 = new GUIStyle();
        labelStyle1.alignment = TextAnchor.UpperLeft;
        labelStyle1.fontStyle = FontStyle.Bold;
        labelStyle1.normal.textColor = Color.white;

        GUIStyle labelStyle2 = new GUIStyle();
        labelStyle2.alignment = TextAnchor.UpperLeft;
        labelStyle2.fontSize = 8;
        labelStyle2.normal.textColor = Color.white;

        //DrawGUI
        //This is a huge piece of code that won't get modified so often
        #region Draw States
        //Group1
        GUI.BeginGroup(new Rect(g1OriginX,g1OriginY,groupWidth,groupHeight));
        GUI.Box(new Rect(0, 0, groupWidth, groupHeight), "Entradas");

        //Draw the 10 Inputs
        //1
        GUI.BeginGroup(new Rect(spacingSubGroup * n, 21, 50, 40));
        if (sI53)
            GUI.Label(new Rect(spacingPictureX, spacingPictureY, 25, 25), mOnBox);
        else
            GUI.Label(new Rect(spacingPictureX, spacingPictureY, 25, 25), mOffBox);
        GUI.Label(new Rect(spacingLabel1X, spacingLabel1Y, 10, 10), "I53", labelStyle1);
        GUI.Label(new Rect(spacingLabel2X, spacingLabel2Y, 50, 20), "Bomba1", labelStyle2);
        GUI.EndGroup();
        //2
        n++;
        GUI.BeginGroup(new Rect(spacingSubGroup * n, 21, 50, 40));
        if (sI51)
            GUI.Label(new Rect(spacingPictureX, spacingPictureY, 25, 25), mOnBox);
        else
            GUI.Label(new Rect(spacingPictureX, spacingPictureY, 25, 25), mOffBox);
        GUI.Label(new Rect(spacingLabel1X, spacingLabel1Y, 10, 10), "I51", labelStyle1);
        GUI.Label(new Rect(spacingLabel2X, spacingLabel2Y, 50, 20), "Bomba2", labelStyle2);
        GUI.EndGroup();
        //3
        n++;
        GUI.BeginGroup(new Rect(spacingSubGroup * n, 21, 50, 40));
        if (sI49)
            GUI.Label(new Rect(spacingPictureX, spacingPictureY, 25, 25), mOnBox);
        else
            GUI.Label(new Rect(spacingPictureX, spacingPictureY, 25, 25), mOffBox);
        GUI.Label(new Rect(spacingLabel1X, spacingLabel1Y, 10, 10), "I49", labelStyle1);
        GUI.Label(new Rect(spacingLabel2X, spacingLabel2Y, 50, 20), "Enfriadores", labelStyle2);
        GUI.EndGroup();
        //4
        n++;
        GUI.BeginGroup(new Rect(spacingSubGroup * n, 21, 50, 40));
        if (sI47)
            GUI.Label(new Rect(spacingPictureX, spacingPictureY, 25, 25), mOnBox);
        else
            GUI.Label(new Rect(spacingPictureX, spacingPictureY, 25, 25), mOffBox);
        GUI.Label(new Rect(spacingLabel1X, spacingLabel1Y, 10, 10), "I47", labelStyle1);
        GUI.Label(new Rect(spacingLabel2X, spacingLabel2Y, 50, 20), "Saca Vaso", labelStyle2);
        GUI.EndGroup();
        //5
        n++;
        GUI.BeginGroup(new Rect(spacingSubGroup * n, 21, 50, 40));
        if (sI45)
            GUI.Label(new Rect(spacingPictureX, spacingPictureY, 25, 25), mOnBox);
        else
            GUI.Label(new Rect(spacingPictureX, spacingPictureY, 25, 25), mOffBox);
        GUI.Label(new Rect(spacingLabel1X, spacingLabel1Y, 10, 10), "I45", labelStyle1);
        GUI.Label(new Rect(spacingLabel2X, spacingLabel2Y, 50, 20), "Motor Banda", labelStyle2);
        GUI.EndGroup();
        //6
        n++;
        GUI.BeginGroup(new Rect(spacingSubGroup * n, 21, 50, 40));
        if (sI43)
            GUI.Label(new Rect(spacingPictureX, spacingPictureY, 25, 25), mOnBox);
        else
            GUI.Label(new Rect(spacingPictureX, spacingPictureY, 25, 25), mOffBox);
        GUI.Label(new Rect(spacingLabel1X, spacingLabel1Y, 10, 10), "I43", labelStyle1);
        GUI.Label(new Rect(spacingLabel2X, spacingLabel2Y, 50, 20), "ValvulaSab1", labelStyle2);
        GUI.EndGroup();
        //7
        n++;
        GUI.BeginGroup(new Rect(spacingSubGroup * n, 21, 50, 40));
        if (sI41)
            GUI.Label(new Rect(spacingPictureX, spacingPictureY, 25, 25), mOnBox);
        else
            GUI.Label(new Rect(spacingPictureX, spacingPictureY, 25, 25), mOffBox);
        GUI.Label(new Rect(spacingLabel1X, spacingLabel1Y, 10, 10), "I41", labelStyle1);
        GUI.Label(new Rect(spacingLabel2X, spacingLabel2Y, 50, 20), "ValvulaSab2", labelStyle2);
        GUI.EndGroup();
        //8
        n++;
        GUI.BeginGroup(new Rect(spacingSubGroup * n, 21, 50, 40));
        if (sI39)
            GUI.Label(new Rect(spacingPictureX, spacingPictureY, 25, 25), mOnBox);
        else
            GUI.Label(new Rect(spacingPictureX, spacingPictureY, 25, 25), mOffBox);
        GUI.Label(new Rect(spacingLabel1X, spacingLabel1Y, 10, 10), "I39", labelStyle1);
        GUI.Label(new Rect(spacingLabel2X, spacingLabel2Y, 50, 20), "Cilindro Tapa", labelStyle2);
        GUI.EndGroup();
        //9
        n++;
        GUI.BeginGroup(new Rect(spacingSubGroup * n, 21, 50, 40));
        if (sI37)
            GUI.Label(new Rect(spacingPictureX, spacingPictureY, 25, 25), mOnBox);
        else
            GUI.Label(new Rect(spacingPictureX, spacingPictureY, 25, 25), mOffBox);
        GUI.Label(new Rect(spacingLabel1X, spacingLabel1Y, 10, 10), "I37", labelStyle1);
        GUI.Label(new Rect(spacingLabel2X, spacingLabel2Y, 50, 20), "   N/A", labelStyle2);
        GUI.EndGroup();
        //10
        n++;
        GUI.BeginGroup(new Rect(spacingSubGroup * n, 21, 50, 40));
        if (sI35)
            GUI.Label(new Rect(spacingPictureX, spacingPictureY, 25, 25), mOnBox);
        else
            GUI.Label(new Rect(spacingPictureX, spacingPictureY, 25, 25), mOffBox);
        GUI.Label(new Rect(spacingLabel1X, spacingLabel1Y, 10, 10), "I35", labelStyle1);
        GUI.Label(new Rect(spacingLabel2X, spacingLabel2Y, 50, 20), "   N/A", labelStyle2);
        GUI.EndGroup();

        GUI.EndGroup();

        //Group2
        n = 1;
        GUI.BeginGroup(new Rect(g2OriginX, g2OriginY, groupWidth, groupHeight));
        GUI.Box(new Rect(0, 0, groupWidth, groupHeight), "Salidas");

        //Draw the 10 Outputs
        //1
        GUI.BeginGroup(new Rect(spacingSubGroup * n, 21, 50, 40));
        if (sO52)
            GUI.Label(new Rect(spacingPictureX, spacingPictureY, 25, 25), mOnBox);
        else
            GUI.Label(new Rect(spacingPictureX, spacingPictureY, 25, 25), mOffBox);
        GUI.Label(new Rect(spacingLabel1X, spacingLabel1Y, 10, 10), "O52", labelStyle1);
        GUI.Label(new Rect(spacingLabel2X, spacingLabel2Y, 50, 20), "Start", labelStyle2);
        GUI.EndGroup();
        //2
        n++;
        GUI.BeginGroup(new Rect(spacingSubGroup * n, 21, 50, 40));
        if (sO50)
            GUI.Label(new Rect(spacingPictureX, spacingPictureY, 25, 25), mOnBox);
        else
            GUI.Label(new Rect(spacingPictureX, spacingPictureY, 25, 25), mOffBox);
        GUI.Label(new Rect(spacingLabel1X, spacingLabel1Y, 10, 10), "O50", labelStyle1);
        GUI.Label(new Rect(spacingLabel2X, spacingLabel2Y, 50, 20), "Mezcla Lista", labelStyle2);
        GUI.EndGroup();
        //3
        n++;
        GUI.BeginGroup(new Rect(spacingSubGroup * n, 21, 50, 40));
        if (sO48)
            GUI.Label(new Rect(spacingPictureX, spacingPictureY, 25, 25), mOnBox);
        else
            GUI.Label(new Rect(spacingPictureX, spacingPictureY, 25, 25), mOffBox);
        GUI.Label(new Rect(spacingLabel1X, spacingLabel1Y, 10, 10), "O48", labelStyle1);
        GUI.Label(new Rect(spacingLabel2X, spacingLabel2Y, 50, 20), "Vaso Banda", labelStyle2);
        GUI.EndGroup();
        //4
        n++;
        GUI.BeginGroup(new Rect(spacingSubGroup * n, 21, 50, 40));
        if (sO46)
            GUI.Label(new Rect(spacingPictureX, spacingPictureY, 25, 25), mOnBox);
        else
            GUI.Label(new Rect(spacingPictureX, spacingPictureY, 25, 25), mOffBox);
        GUI.Label(new Rect(spacingLabel1X, spacingLabel1Y, 10, 10), "O46", labelStyle1);
        GUI.Label(new Rect(spacingLabel2X, spacingLabel2Y, 50, 20), "S.Sabor1", labelStyle2);
        GUI.EndGroup();
        //5
        n++;
        GUI.BeginGroup(new Rect(spacingSubGroup * n, 21, 50, 40));
        if (sO44)
            GUI.Label(new Rect(spacingPictureX, spacingPictureY, 25, 25), mOnBox);
        else
            GUI.Label(new Rect(spacingPictureX, spacingPictureY, 25, 25), mOffBox);
        GUI.Label(new Rect(spacingLabel1X, spacingLabel1Y, 10, 10), "O44", labelStyle1);
        GUI.Label(new Rect(spacingLabel2X, spacingLabel2Y, 50, 20), "Sabor1 Lleno", labelStyle2);
        GUI.EndGroup();
        //6
        n++;
        GUI.BeginGroup(new Rect(spacingSubGroup * n, 21, 50, 40));
        if (sO42)
            GUI.Label(new Rect(spacingPictureX, spacingPictureY, 25, 25), mOnBox);
        else
            GUI.Label(new Rect(spacingPictureX, spacingPictureY, 25, 25), mOffBox);
        GUI.Label(new Rect(spacingLabel1X, spacingLabel1Y, 10, 10), "O42", labelStyle1);
        GUI.Label(new Rect(spacingLabel2X, spacingLabel2Y, 50, 20), "S.Sabor2", labelStyle2);
        GUI.EndGroup();
        //7
        n++;
        GUI.BeginGroup(new Rect(spacingSubGroup * n, 21, 50, 40));
        if (sO40)
            GUI.Label(new Rect(spacingPictureX, spacingPictureY, 25, 25), mOnBox);
        else
            GUI.Label(new Rect(spacingPictureX, spacingPictureY, 25, 25), mOffBox);
        GUI.Label(new Rect(spacingLabel1X, spacingLabel1Y, 10, 10), "O40", labelStyle1);
        GUI.Label(new Rect(spacingLabel2X, spacingLabel2Y, 50, 20), "Sabor2 Lleno", labelStyle2);
        GUI.EndGroup();
        //8
        n++;
        GUI.BeginGroup(new Rect(spacingSubGroup * n, 21, 50, 40));
        if (sO38)
            GUI.Label(new Rect(spacingPictureX, spacingPictureY, 25, 25), mOnBox);
        else
            GUI.Label(new Rect(spacingPictureX, spacingPictureY, 25, 25), mOffBox);
        GUI.Label(new Rect(spacingLabel1X, spacingLabel1Y, 10, 10), "O38", labelStyle1);
        GUI.Label(new Rect(spacingLabel2X, spacingLabel2Y, 50, 20), "Sensor Tapa", labelStyle2);
        GUI.EndGroup();
        //9
        n++;
        GUI.BeginGroup(new Rect(spacingSubGroup * n, 21, 50, 40));
        if (sO36)
            GUI.Label(new Rect(spacingPictureX, spacingPictureY, 25, 25), mOnBox);
        else
            GUI.Label(new Rect(spacingPictureX, spacingPictureY, 25, 25), mOffBox);
        GUI.Label(new Rect(spacingLabel1X, spacingLabel1Y, 10, 10), "O36", labelStyle1);
        GUI.Label(new Rect(spacingLabel2X, spacingLabel2Y, 50, 20), "   N/A", labelStyle2);
        GUI.EndGroup();
        //10
        n++;
        GUI.BeginGroup(new Rect(spacingSubGroup * n, 21, 50, 40));
        if (sO34)
            GUI.Label(new Rect(spacingPictureX, spacingPictureY, 25, 25), mOnBox);
        else
            GUI.Label(new Rect(spacingPictureX, spacingPictureY, 25, 25), mOffBox);
        GUI.Label(new Rect(spacingLabel1X, spacingLabel1Y, 10, 10), "O34", labelStyle1);
        GUI.Label(new Rect(spacingLabel2X, spacingLabel2Y, 50, 20), "   N/A", labelStyle2);
        GUI.EndGroup();

        GUI.EndGroup();
        #endregion


        GUI.Label(new Rect(10, 500, 50, 20),showOnGUI);
        //GUI.Box(new Rect(5, 72, 500, 70), "Salidas");


        //Start Button
        if (sO52 == false)
        {
            if (GUI.Button(new Rect(Screen.width -100, Screen.height -50, 95, 40), "Start")) 
            {
                sO52 = true;
                //Debug.Log("Tick On");
            }
        }
        else
        {
            if (GUI.Button(new Rect(Screen.width - 100, Screen.height - 50, 95, 40), "UnStart"))  
            {
                sO52 = false;
                //Debug.Log("Tick Off");
            }
        }
        
    }

    //Action To take when the application closes
    //A.K.A. Close The freaking serial port before I lose my mind.
    void OnApplicationQuit()
    {
        if (sPort != null)
        {
            if (sPort.IsOpen)
            {
                sPort.Close();
            }
        }
    }
}
