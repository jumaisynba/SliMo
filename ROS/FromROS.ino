
#include <FIR.h>

#include <TeensyThreads.h>

#include <ros.h>


#include <std_msgs/Float64.h>
#include <std_msgs/String.h>
#include <std_msgs/Int32.h>

#include <geometry_msgs/WrenchStamped.h>
#define SAMPLING_RATE_KHZ 10
IntervalTimer myTimer;


float phase = 0.0;
float freq = 100;
float value = 0.0;


float d_phase1 = 0.1;
float d_phase2 = 0.5;
float d_phase3 = 1;

float d_phase = 0;


float twopi = 3.13159 * 2;
int vIn;


float voltage;
float LastVoltage;
float rate;
float curT;

float filtR = 0.0;

unsigned long lastTime = 0;

unsigned long dt = 0; // dt in milliseconds

float wait = 1000;




ros::NodeHandle nh;
//geometry_msgs::WrenchStamped msg;
float varData = 0;
float rotData = 0;

bool thereIsAChange = false;

float normRate = 0;

int distanceLim = -30;

int curState = 0;

String modeString;
int modeInt = 0;
void messageCB(const geometry_msgs::WrenchStamped& msg) {
  varData = msg.wrench.force.z;
  rotData = msg.wrench.torque.z;
  char result[13];


  dtostrf(normRate , 13, 10, result);

  if (thereIsAChange) {
    nh.loginfo(result);

  }
}

void messageCb(const std_msgs:: String &msg) {
  modeString = msg.data;
  char result2[13];
  modeInt = modeString.toInt();

  dtostrf(modeInt , 13, 10, result2);

  nh.loginfo(result2);
}


std_msgs::String str_msg;
//std_msgs::Int32 int_msgs;
ros::Subscriber<geometry_msgs::WrenchStamped> sub("/wrench", &messageCB);

////complete subscriber
//ros::Subscriber sub("modder",&msg ,&modeSUB);

ros::Subscriber<std_msgs::String> s("modder", &messageCb);

//ros::Publisher limiter("limiter", &int_msgs);


static float filterloop(float in) {
  static float flt = 0.;
  static float a = .01;
  flt = a * in + (1. - a) * flt;
  return flt;

}


void setup() {
  analogWriteResolution(12);
  analogReadResolution(12);

  //Serial1.begin(9600);
  nh.initNode();



  int time_us;
  time_us = 1000 / SAMPLING_RATE_KHZ;
  myTimer.begin(teensy, time_us);

}

void loop() {
  //Serial1.println(varData);
  nh.subscribe(sub);
  nh.subscribe(s);
  nh.spinOnce();

  //delay(2);
  if (millis() - lastTime  >= dt)   // wait for dt milliseconds
  {
    lastTime = millis();
    float sensorValue;
    if (varData >= distanceLim && varData <= -23.5) {
      sensorValue = varData;
    } else {
      sensorValue = 0;

    }
    voltage = sensorValue; // this line changed !!

    rate = (voltage - LastVoltage) / 10;


    LastVoltage = voltage;

    normRate = abs(((rate + 3.5) / 7) - 0.5) ;

  }


  if (varData < 10) {
    vIn = 0;
  }
  //char result2[13];



  //if (millis() - curT >= wait && rotData > 0.7) {
  //dtostrf(distanceLim , 13, 10, result2);

  //curT = millis();
  //nh.loginfo("changed phase mode");

  if (modeInt == 1 ) {
    dt = 0;
    d_phase = 0;
    curState = 1;
    distanceLim = -30;

    //int_msgs.data = distanceLim;
    //limiter.publish(&int_msgs);

  } else if (modeInt == 10 ) {
    dt = 5;
    d_phase = 0.5;
    curState = 10;
    distanceLim = -30;




  } else if (modeInt == 3) {
    dt = 0;
    d_phase = 0;
    curState = 3;
    distanceLim = -40;

  } else if (modeInt == 8) {
    dt = 2;
    d_phase = 0.1;
    curState = 8;
    distanceLim = -35;

  } else if (modeInt == 12) {
    dt = 5;
    d_phase = 0.5;
    curState = 12;
    distanceLim = -40;



  } else if (modeInt == 2) {
    dt = 0;
    d_phase = 0;
    curState = 2;
    distanceLim = -35;



  } else if (modeInt == 7) {
    dt = 2;
    d_phase = 0.1;
    curState = 7;
    distanceLim = -30;


  } else if (modeInt == 6) {
    dt = 10;
    d_phase = 0.1;
    curState = 6;
    distanceLim = -40;



  } else if (modeInt == 9) {
    dt = 2;
    d_phase = 0.1;
    curState = 9;
    distanceLim = -40;



  } else if (modeInt == 11) {
    dt = 5;
    d_phase = 0.5;
    curState = 11;
    distanceLim = -35;



  } else if (modeInt == 4) {
    dt = 10;
    d_phase = 0.1;
    curState = 4;
    distanceLim = -30;



  } else if (modeInt == 5) {
    dt = 10;
    d_phase = 0.1;
    curState = 5;
    distanceLim = -35;



  } else if (modeInt == 0) {
    dt = 0;
    d_phase = 0;
    curState = 0;
    distanceLim = 0;


  }

}

void teensy(void) {
  if (rate != 0.0) {
    filtR = filterloop(normRate);
  } else {
    filtR = 0.0;
  }



  //value = sin(phase)*varData*50;

  //coefficients for d_phase=0.1
  //4000 max = 7.1V (PtP) no amp
  //900-950 max = 11.1 - 11.3V(PtP) DC RMS-FS = 3.47V with amp


  //abs(rate)<= 23 &&
  if (abs(rate) != 0.0 && normRate >= 0.01 && normRate <= 0.3) {

    value = sin(phase * abs(1 + filtR) + twopi) * 300 + 300; //+2048;
    thereIsAChange = true;
  } else {
    value = 0;
    thereIsAChange = false;
  }



  phase = phase + d_phase;
  if (phase > twopi) {
    phase = 0;
  }
  analogWrite(A21, (int)value);
  analogWrite(A22, (int)value);


}
