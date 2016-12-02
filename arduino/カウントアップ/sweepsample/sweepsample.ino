
const int led_pin = 9;

void setup() {
}

void loop() {
  
  for ( int led_value = 102; led_value < 256; led_value += 3 ) {
    analogWrite( led_pin, led_value );
    delay(40);
  }

//analogWrite( led_pin, 0 );
//delay(1000);
 
}
		
