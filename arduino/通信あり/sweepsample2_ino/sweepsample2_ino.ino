int data = 0;
int count = 0;
const int led_pin = 9;

void setup() {
  Serial.begin(19200);
}

void loop() {
  if (Serial.available() > 0) {
    data = Serial.read();
  }
  
  if (count == 1) {
    if (data == 's') {
      for ( int led_value = 254; led_value > 100; led_value -= 3 ) {
        analogWrite( led_pin, led_value );
        delay(50);
        if (led_value <= 100) {
          count += 1;
          analogWrite( led_pin, 0);
        }
      }
    }
  }
  
  if (data == 'a') {
    for ( int led_value = 254; led_value > 100; led_value -= 3 ) {
      analogWrite( led_pin, led_value );
      delay(50);
    }
  }

  if (data == 'd') {
    analogWrite(led_pin, 0);
    count = 0;
  }

Serial.flush();
}

