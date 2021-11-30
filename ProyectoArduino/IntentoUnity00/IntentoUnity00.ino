// STATES ARRAY
// The following array contains the states of the pins of the
// MEGA2560, it is necessary to understand the loops in which
// each pin is checked since they have DIFFERENT physical name
// than the index of the array.

int lastState [20] = { };

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

// the setup routine runs once when you press reset:
void setup() {                
  
  // Set arduino MEGA 2560 even pins as outputs
  for(i=52;i>=34;i-=2){
    pinMode(i, OUTPUT);
  }
  // Set arduino MEGA 2560 odd pins as inputs
  for(i=53;i>=34;i-=2){
    pinMode(i, INPUT);
  }
  Serial.begin(115200);
  inputString.reserve(20);
}

// The loop routine runs over and over again forever:
void loop() {
  
  //READ digital inputs on the board
  // then change states accordingly.
  for (i=1; i<20 ; i+=2 ){//starts odd numbers since those are digital inputs 
    inputPin=i+34;
    inputState = digitalRead(inputPin);
    if (inputState != lastState[i]) {
      if (inputState == HIGH) {
        sendState(inputPin, '1');
        //delay(20);
      }
      else{
        sendState(inputPin, '0');
        //delay(20);
      }
      lastState[i]=inputState;
    }
  }
  
  //READ analog input 
  int sensorValue = analogRead(A0);
  
  if (stringComplete) {
     if(inputString[0]=='s'){
       outputPin=(int)inputString[1];
       outputState=(int)inputString[2] - 48;
       digitalWrite(outputPin, outputState);
     }
    // clear the string:
    inputString = "";
    stringComplete = false;
  }
  
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
