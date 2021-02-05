  #include <FIR.h>

#include <TeensyThreads.h>

#include <ros.h>


#include <std_msgs/Float64.h>
#include <geometry_msgs/WrenchStamped.h>
#define SAMPLING_RATE_KHZ 10
IntervalTimer myTimer;


float phase = 0.0;
float freq = 100;
float d_phase = 0.1;

float twopi = 3.13159 * 2;
int vIn;


float voltage;
float LastVoltage;
float rate;

unsigned long lastTime = 0;

unsigned long dt = 1; // dt in milliseconds

ros::NodeHandle nh;
//geometry_msgs::WrenchStamped msg;
float varData = 0;




void messageCB(const geometry_msgs::WrenchStamped& msg) {
  varData = msg.wrench.force.z;
  char result[8];
  dtostrf(rate , 6, 2, result);
  if (rate != 0.00){
      nh.loginfo(result);
  }

}




ros::Subscriber<geometry_msgs::WrenchStamped> sub("/wrench", &messageCB);
void setup() {
  analogWriteResolution(12);
  analogReadResolution(12);
  
  //Serial1.begin(9600);
  nh.initNode();
  //Serial1.println("Subscribed");

  int time_us;
  time_us = 1000 / SAMPLING_RATE_KHZ;
  myTimer.begin(teensy, time_us);

}

void loop() {
  //Serial1.println(varData);
  nh.subscribe(sub);

  nh.spinOnce();

  //delay(2);
    if (millis() - lastTime  >= dt)   // wait for dt milliseconds
 {
   lastTime = millis();
   int sensorValue;
   if (varData >= -30.0){
    sensorValue = 0; 
   }else{
   sensorValue = varData;

   }
   voltage = sensorValue; // this line changed !!
   
   rate = (voltage-LastVoltage);
   
   LastVoltage = voltage;

 }

    
  if (varData<10){
    vIn = 0;  
  }
}

void teensy(void){


  float value;
  
  float value2;

  //value = sin(phase)*varData*50;
  
  //coefficients for d_phase=0.1
  //4000 max = 7.1V (PtP) no amp
  //900-950 max = 11.1 - 11.3V(PtP) DC RMS-FS = 3.47V with amp 
  //if (rate < 0.0){
    value = sin(phase*abs(rate))*400+2048;
  //}
  phase = phase + d_phase;
  value2 = sin(phase + twopi)*400+2048;
   if (phase > twopi){
    phase = 0;
   }
  analogWrite(A21,(int)value);
  analogWrite(A22,(int)value);
   

}
