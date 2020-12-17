#include <Arduino.h>

#include "TeensyThreads.h"

#define fsrAnalogPin  14
#define SAMPLING_RATE_KHZ 10 // in KHz
IntervalTimer myTimer;

int fsrReading=0;
int m = 0;
const long milliInterval = 50; //delay for sending data to Unity (miliseconds)
unsigned long previousMillis = 0;


const long microInterval = 125; //delay for sending data to Hapuator (microseconds)
unsigned long previousMicros = 0;

unsigned long p = 0;

unsigned long prev = 0; //used for delay in button reading




float phase = 0.0;
float twopi = 3.14159 * 2;
float Vamp = 50;
float Vref = 0;




float d_phase = 0.01 ;


float delta;

void setup() {
  analogWriteResolution(12);
  analogReadResolution(12);

  pinMode(fsrAnalogPin, INPUT);
  Serial.begin(9600);

  int time_us;

  time_us = 1000/SAMPLING_RATE_KHZ;
  myTimer.begin(teensy, time_us); // Task1 runs at every 100us or 10KHz

}
void loop() {
  fsrReading = analogRead(fsrAnalogPin);
  //Serial.println(fsrReading); 
}

void teensy(void){
    float val;
    //Vamp = sin(TWO_PI*fsrReading);
    val = -sin(phase)*fsrReading;
    phase = phase + d_phase;
    if (phase > twopi){
      phase = 0;
    }
    Serial.println(fsrReading);
    analogWrite(A14, (int)val); 
}
                                                                   
