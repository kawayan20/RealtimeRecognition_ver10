#include <TimerOne.h>
//タイマー割り込みの関係でデジタルピンの9と10番ピンは使えない

#define AX 0
#define AY 1
#define AZ 2
#define ST 108
#define FI 255
#define PWM_DERAY 20

#define moterPin 6 // 9,10以外のピン

signed short x = 0, y = 0, z = 0;
unsigned long t_accel = 0;
unsigned long t0 = 0;
int tmp = ST;


void setup()
{
  Serial.begin(115200);
  pinMode(moterPin, OUTPUT);

  Timer1.initialize();
  Timer1.attachInterrupt(accelread, 2000);
}

void mywrite(short src) {
  Serial.write((src & 0xFF00) >> 8);
  Serial.write(src & 0xFF);
}

void t_mywrite(long src) {
  Serial.write((src & 0xFF000000) >> 24);
  Serial.write((src & 0xFF0000) >> 16);
  Serial.write((src & 0xFF00) >> 8);
  Serial.write(src &  0xFF);
}

void accelread() {

  x = analogRead(AX); //100マイクロ秒
  y = analogRead(AY); //100マイクロ秒
  z = analogRead(AZ); //100マイクロ秒

  t_accel = micros();
  t_accel = t_accel / 1000;

  x = map(x, 0, 1023, 0, 5000) - 2500;
  y = map(y, 0, 1023, 0, 5000) - 2500;
  z = map(z, 0, 1023, 0, 5000) - 2500;

  //WAA006のsenb  加速度送信用ヘッダ
  Serial.write(0x73);
  Serial.write(0x65);
  Serial.write(0x6E);
  Serial.write(0x62);

  //時間送信
  t_mywrite(t_accel);

  //加速度データ送信
  mywrite(x);
  mywrite(y);
  mywrite(z);

  //終端マーク
  Serial.write(0xC1);

  if ( Serial.available() > 0 ) {
    if ( Serial.read() == 'A' ) {
      while (1) {
        if (Serial.read() == 'B') {
          break;
        }
      }
    }
  }
}


void vibmoter() {
  
  if(millis() - t0 < PWM_DERAY){
    analogWrite( moterPin, tmp );
  }else{
    t0 = millis();
    tmp += 3;
    analogWrite( moterPin, tmp );
  }

  if(tmp >= FI){
    tmp = ST;
  }
}

void loop()
{
  vibmoter();
}


