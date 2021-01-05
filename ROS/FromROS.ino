#include <TeensyThreads.h>

#include <ros.h>


#include <std_msgs/Float64.h>
#include <geometry_msgs/WrenchStamped.h>
#define SAMPLING_RATE_KHZ 10
IntervalTimer myTimer;


float phase = 0.0;
float d_phase = 0.05;

float twopi = 3.13159 * 2;

ros::NodeHandle nh;
//geometry_msgs::WrenchStamped msg;
float varData = 0;




void messageCB(const geometry_msgs::WrenchStamped& msg) {
  varData = msg.wrench.force.z;
  char result[8];
  dtostrf(varData , 6, 2, result);
  nh.loginfo(result);
}




ros::Subscriber<geometry_msgs::WrenchStamped> sub("/wrench", &messageCB);
void setup() {
  analogWriteResolution(12);
  analogReadResolution(12);
  
  //Serial1.begin(9600);
  nh.initNode();
  nh.subscribe(sub);
  //Serial1.println("Subscribed");

  int time_us;
  time_us = 1000 / SAMPLING_RATE_KHZ;
  myTimer.begin(teensy, time_us);

}

void loop() {
  //Serial1.println(varData);

  nh.spinOnce();

  //delay(2);
}

void teensy(void){

float value;


value = sin(phase)*varData*50;

phase = phase +d_phase;

 if (phase > twopi){
  phase = 0;
 }
analogWrite(A21,(int)value);
analogWrite(A22,-(int)value);
 

}
