int data = 0;
int count = 0;
const int led_pin = 9;

void setup() {
  Serial.begin(19200);
}

void loop() {
  if (Serial.available() > 0) {
    switch (Serial.read()) {
      /////////////////////////////////////////////振動1回2048_1024_40_3//////////////////////////////////////////
      case 's':
        for ( int led_value = 255; led_value >= 102; led_value -= 3 ) {
          if ( led_value <= 102 ) {
            led_value = 102;
            analogWrite( led_pin, led_value );
            delay(8);
          } else {
            analogWrite( led_pin, led_value );
            delay(40);
          }
        }
        analogWrite( led_pin, 0);
        break;
      ///////////////////////////////////////////振動2回1024*2_512*2_40_6//////////////////////////////////////////
      case 'o':
        for ( int led_value = 252; led_value >= 102; led_value -= 6 ) {
          if ( led_value == 102 ) {
            led_value = 102;
            analogWrite( led_pin, led_value );
            delay(24);
          } else {
            analogWrite( led_pin, led_value );
            delay(40);
          }
        }

       analogWrite(led_pin, 0);
//       delay(40);
               
        for ( int led_value = 252; led_value >= 102; led_value -= 6 ) {
          if ( led_value == 102 ) {
            led_value = 102;
            analogWrite( led_pin, led_value );
            delay(24);
          } else {
            analogWrite( led_pin, led_value );
            delay(40);
          }
        }
        analogWrite( led_pin, 0);
        break;
      ///////////////////////////////////////////////振動1回 2048_ 1024_ 50_ 3.76//////////////////////////////
      case 'p':
        for (int i = 10200; i <= 25500; i += 376)
        {
          double x;

          if (i == 25240)
          {
            x = 255.0;
            analogWrite(led_pin, round(x));
            delay(48);
          }
          else
          {
            x = i / 100.0;
            analogWrite(led_pin, round(x));
            delay(50);
          }
        }
        analogWrite( led_pin, 0);
        break;
      //////////////////////////////////////////////振動2回 1024*2_512*2_50_7.52/////////////////////////
      case 'u':
        for (int i = 10200; i <= 25500; i += 752) {
          double x;
          if (i == 25240) {
            x = 255.0;
            analogWrite(led_pin, round(x));
            delay(24);
          } else {
            x = i / 100.0;
            analogWrite(led_pin, round(x));
            delay(50);
          }
        }
        for (int i = 10200; i <= 25500; i += 752) {
          double x;
          if (i == 25240) {
            x = 255.0;
            analogWrite(led_pin, round(x));
            delay(24);
          } else {
            x = i / 100.0;
            analogWrite(led_pin, round(x));
            delay(50);
          }
        }
        analogWrite( led_pin, 0);
        break;

      //////////////////////////////////////////////
      case 'a':
        analogWrite( led_pin, 0);
        break;

      default: break;
    }
  }
}
