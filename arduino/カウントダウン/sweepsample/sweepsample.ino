
// led_brightness_sample1

const int led_pin = 9;

void setup() {
}

void loop() {
//
  for ( int led_value = 250; led_value > 100; led_value -= 3 ) {
    analogWrite( 6, led_value );
    delay(40);
  }

    
   //analogWrite( 6, 150 );
  
  
}

