#include "Timer.h"

// STATES ARRAY
// The following array contains the states of the pins of the
// MEGA2560, it is necessary to understand the loops in which
// each pin is checked since they have DIFFERENT physical name
// than the index of the array.
Timer t;                   //starts create a timer object

int lastState [32] = { };

const int analogInPin = A0;  // Analog input pin that the potentiometer is attached to
const int analogOutPin = 9; // Analog output pin that the LED is attached to

int inputValue = 0;        // value read from the potentiometer
int outputValue = 0;       // value output to the PWM (analog out)

float uVal00 = 0;          // raw analog value in volts
float uVal01 = 0;          // Holds the last variable for use de dif_ec
float yVal00= 0;           // output value after difference equation
float yVal01= 0;           // Holds the last variable for use de dif_ec

// A variable to loop over :)
int i=0;
// A variable to compare states
int inputState=0;
int outputState=0;
// Reserve a variable for the pin that being used
int inputPin=0;
int outputPin=0;
// Declared on "Serial event" example.
String inputString = "";         // a string to hold incoming data
boolean stringComplete = false;  // whether the string is complete

// function declaration
void sendState(int ip, int s);

int lRepeat=0;
char sRepeat;

// the setup routine runs once when you press reset:
void setup() {                
  
  // Set arduino MEGA 2560 even pins as outputs
  for(i=52;i>=22;i-=2){
    pinMode(i, OUTPUT);
  }
  // Set arduino MEGA 2560 odd pins as inputs
  for(i=53;i>=22;i-=2){
    pinMode(i, INPUT);
  }
  Serial.begin(115200);
  inputString.reserve(20);
  
  //Setup timer function
  t.every(250, sendReading);
}

// The loop routine runs over and over again forever:
void loop() {
  
  //READ digital inputs on the board
  // then change states accordingly.
  for (i=1; i<32 ; i+=2 ){//starts odd numbers since those are digital inputs 
    inputPin=i+34;
    inputState = digitalRead(inputPin);
    if (inputState != lastState[i]) {
      if (inputState == HIGH) {
        sendState(inputPin, '1');
        lRepeat=inputPin;
        sRepeat='1';
        //delay(20);
      }
      else{
        sendState(inputPin, '0');
        lRepeat=inputPin;
        sRepeat='0';
        //delay(20);
      }
      lastState[i]=inputState;
    }
  }
  
  //READ analog input EMULATE A PLANT
  // read the analog in value:
  inputValue = analogRead(analogInPin);            
  // map it to the range of the analog out:
  uVal00 = inputValue*0.005; //Maps to 5v
  //Diferential eq.
  //yVal00 = yVal01*0.8953 + uVal01*0.0085 + uVal00*0.015;
  yVal00 = yVal01*0.8953 + uVal01*0.0085 + uVal00*0.0085;
  //yVal00 = yVal01*1.902 + uVal01*0.0025 + uVal00*0.0015;
  //Prevent windup
  if (yVal00>5){yVal00=5;}
  if (yVal00<0){yVal00=0;}
  //Hold values
  uVal01 = uVal00;
  yVal01 = yVal00;
  outputValue = (int) (yVal00*204.6);
  // change the analog out value:
  analogWrite(analogOutPin, outputValue);
  
  //When recives a serial command from Unity runs this piece 
  //of code
  if (stringComplete) {
     if(inputString[0]=='s'){
       outputPin=(int)inputString[1];
       outputState=(int)inputString[2] - 48;
       digitalWrite(outputPin, outputState);
     }
     //else if(inputString[0]=='e'){resendLast();
     //}
     //else{Serial.println('e');}
    // clear the string:
    inputString = "";
    stringComplete = false;
  }
  
  //checks timer to do the one thing it has to do
  t.update();
  
}

// sendState(int ip, char s) this function is usually called when
// a digital input has changed its state, to send the information 
// to Unity over serial port.
// arguments:
// int ip : Stands for input port... that number will be sent to
// identify which pin has changed.
// char s : The state, 0 or 1.
void sendState(int ip, char s){
  Serial.write('s'); //This one stands for state, for identification
  Serial.write(ip);
  Serial.write(s);
  //Serial.write(255);//Send this as end of stream
  //Serial.write(10);//Comment this one
  Serial.println();
  //Serial.flush();
}

// This serial event recives data from unity and changes states
// of the outputs accordingly.
void serialEvent() {
  while (Serial.available()) {
    // get the new byte:
    char inChar = (char)Serial.read(); 
    // add it to the inputString:
    inputString += inChar;
    // if the incoming character is a newline, set a flag
    // so the main loop can do something about it:
    if (inChar == '\n') {
      stringComplete = true;
    } 
  }
}

//Timer event
void sendReading()
{
  Serial.write('a');
  Serial.write('n');
  Serial.println(inputValue);
}
void resendLast(){
  sendState(lRepeat, sRepeat);
}

