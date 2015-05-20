/*
* Daniel Carlozzi @2015
* GoGreen Bike Interface
*/
// initialize the library with the numbers of the interface pins
#define arrayLength(x) (sizeof(x) / sizeof(x[0]))

//leds pins
const int ledPins[1] = {
  6 //pins
};
const String ledColors[1] = {
  "red"
};
//buttons pins RESET
const int buttonPins[1] = {
  3 //pins
};
int lastButtonState[1] = {
  LOW
};
int buttonsDebounce[1] = {
  0
};
const int buttonsDebounceTime = 100;
const String buttonActions[1] = {
  "reset"
};

unsigned long syncLastTime;
bool inSync;
/***************************************************************/
void setup() {

  Serial.begin(19200); //set serial to 9600 baud rate
  Serial.setTimeout(100); //250 milis until close

  //buttons
  for (int i = 0; i < 3; i++) {
    pinMode(buttonPins[i], INPUT);
    buttonsDebounce[i] = millis();
  }

  //leds
  for (int i = 0; i < 2; i++) {
    pinMode(ledPins[i], OUTPUT);
  }

  syncLastTime = 0;
  inSync = true;
}

/***************************************************************/
String buildResponse(String type, boolean suceeded, String value)
{
  String _pfx = "{\"cmd\":\"" + type + "\"";
  if (suceeded)
    _pfx += ",succeeded:true";
  else
    _pfx += ",succeeded:false";

  return _pfx + ",val:\"" + value + "\"}";
}

/***************************************************************/
void checkSync()
{
  if ((millis() - syncLastTime) > (5000))
  {
    if (inSync)
    {
      ledLoop("{\"Led\":\"0\"}"); //turn on warning led
    }
    inSync = false;
  }
}
/***************************************************************/
void loop() {
  if (Serial.available() > 0) // Check to see if there is a new message
  {
    syncLastTime = millis(); //all messages are considered sync signals

    String message;
    message = Serial.readString(); // Put the serial input into the message

    //sync
    syncLoop(message);

    if (inSync)
    {
      //leds
      ledLoop(message);

      //commands
      systemLoop(message);
    }
  }

  //buttons
  buttonLoop();

  delay(10);

  //check sync
  checkSync();

}
/***************************************************************
{"Sync":""}
***************************************************************/
void syncLoop(String message)
{
  if (message.startsWith("{\"Sync\":\""))//{"Sync":""}
  {
    if (!inSync)
    {
      inSync = true;
      ledLoop("{\"Led\":\"-0\"}"); //turn off warning led
    }
    String _cj = buildResponse("Sync", true, "OK");
    Serial.println(_cj);
  }
}
/***************************************************************
{"Led":"Off"}
{"Led":"1"}
{"Led":"-0"}
***************************************************************/
void ledLoop(String message)
{
  if (message.startsWith("{\"Led\":\""))//{"Led":"Off"|"0"|"1"}
  {
    String _cj = "";
    int openBracket = message.indexOf("\":\"");
    int closeBracket = message.indexOf("\"}");
    String text = message.substring(openBracket + 3, closeBracket);

    if (text == "Off") {
      for (int i = 0; i < arrayLength(ledPins); i++) {
        digitalWrite(ledPins[i], LOW);
      }
      _cj = buildResponse("Led", true, "OK");
    }
    else
    {
      int ledIdx = text.toInt();
      if (abs(ledIdx) < arrayLength(ledPins))
      {
        //turn off led: -0,-1,-2
        if (text.startsWith("-"))
        {
          digitalWrite(ledPins[abs(ledIdx)], LOW);
        }
        //turn on led: 0,1,2
        else
        {
          digitalWrite(ledPins[ledIdx], HIGH);
        }
        _cj = buildResponse("Led", true, "OK");
      }
      else
      {
        _cj = buildResponse("Led", false, "Invalid Led Index");
      }
    }

    Serial.println(_cj);
  }
}
/***************************************************************/
void buttonLoop()
{
  //action
  for (int i = 0; i < 3; i++)
  {
    int _bs = digitalRead(buttonPins[i]);
    if (_bs != lastButtonState[i])
    {
      if (_bs == HIGH && (millis() - buttonsDebounce[i]  > buttonsDebounceTime)) {
        Serial.println(buildResponse("Button", true, buttonActions[i]));
        buttonsDebounce[i] = millis();
      }
      lastButtonState[i] = _bs;
    }
  }

}
/***************************************************************
{"System":"Capabilities"}
***************************************************************/
void systemLoop(String message)
{
  if (message.startsWith("{\"System\":\"Capabilities\"}"))
  {
    String _cj = "{";
    _cj += "\"System\":{\"Model\":\"Nano Clon\"},";

    String colors = "";
    for (int i = 0; i < arrayLength(ledColors); i++) {
      colors += ledColors[i] + ",";
    }
    _cj += "\"Leds\":{\"Capable\":false,\"Count\":" + String(arrayLength(ledPins)) + ",\"Colors\":\"" + colors.substring(0, colors.length() - 1) + "\"},";

    String actions = "";
    for (int i = 0; i < arrayLength(buttonActions); i++) {
      actions += buttonActions[i] + ",";
    }
    _cj += "\"Buttons\":{\"Capable\":true,\"Count\":" + String(arrayLength(buttonPins)) + ",\"Actions\":\"" + actions.substring(0, actions.length() - 1) + "\"},";
    _cj += "\"Relays\":{\"Capable\":false,\"Count\":0}";
    _cj += "}";
    Serial.println(_cj);
  }
}
/***************************************************************/




























