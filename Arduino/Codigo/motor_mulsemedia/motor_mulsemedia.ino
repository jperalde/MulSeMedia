
#define ENABLE 5
#define DIRA 3
#define DIRB 4
#include<Uduino.h>
Uduino uduino("FanBoard");

void setup() {
  //---set pin direction
  pinMode(ENABLE,OUTPUT);
  pinMode(DIRA,OUTPUT);
  pinMode(DIRB,OUTPUT);
  Serial.begin(9600);

  uduino.addCommand("turnOn",turnOnFan);
  // uduino.addCommand("turnOff",turnOffFan);
}
void turnOnFan(){
  digitalWrite(ENABLE,HIGH ); //enable on
  digitalWrite(DIRB,HIGH); //one way
  digitalWrite(DIRA,LOW);
  delay(5000);
  digitalWrite(ENABLE,LOW);
}
/*void turnOffFan(){
   digitalWrite(ENABLE,LOW); //all done
   digitalWrite(DIRA,LOW);
}*/

void loop() {

  uduino.update();
  delay(10);
}
