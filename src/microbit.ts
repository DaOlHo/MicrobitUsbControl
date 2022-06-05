// Open a channel large enough
serial.redirectToUSB();
serial.setRxBufferSize(64);
serial.setTxBufferSize(64);

// Report system info
serial.writeLine(`Info.ver ${control.hardwareVersion()}`);
serial.writeLine(`Info.devName ${control.deviceName()}`);
serial.writeLine(`Info.serialNum ${control.deviceSerialNumber()}`);

// Class that holds all display related functions
class Display {
    static matrix(input: string) {
        input = input.replaceAll(' ', '').trim();
        basic.clearScreen();

        let stringIndex = 0;

        for (let y = 0; y < 5; y++) {
            for (let x = 0; x < 5; x++) {
                if (input.charAt(stringIndex) == "1") led.plot(x, y);

                stringIndex++;
            }
        }
    }

    static text(input: string) {
        basic.showString(input);
    }

    static clear() {
        basic.clearScreen();
    }
}

// Register all events to report themselves
input.onButtonPressed(Button.A, function () { serial.writeLine("btnPressed.A"); });
input.onButtonPressed(Button.B, function () { serial.writeLine("btnPressed.B"); });
input.onButtonPressed(Button.AB, function () { serial.writeLine("btnPressed.AB"); });
input.onPinPressed(TouchPin.P0, function () { serial.writeLine("pinPressed.P0"); });
input.onPinPressed(TouchPin.P1, function () { serial.writeLine("pinPressed.P1"); });
input.onPinPressed(TouchPin.P2, function () { serial.writeLine("pinPressed.P2"); });
// input.onLogoEvent(TouchButtonEvent.Pressed, function () { serial.writeLine("logoPressed"); }); // Only works on V2 microbits

// Report sensor data every second
basic.forever(function () {
    serial.writeLine(`Sensors.temp ${input.temperature()}`);
    serial.writeLine(`Sensors.light ${input.lightLevel()}`);
    serial.writeLine(`Sensors.accelX ${input.acceleration(Dimension.X)}`);
    serial.writeLine(`Sensors.accelY ${input.acceleration(Dimension.Y)}`);
    serial.writeLine(`Sensors.accelZ ${input.acceleration(Dimension.Z)}`);
//    serial.writeLine(`Sensors.sound ${input.soundLevel()}`); // Only works on V2 microbits
    serial.writeLine(`Sensors.rotPitch ${input.rotation(Rotation.Pitch)}`);
    serial.writeLine(`Sensors.rotRoll ${input.rotation(Rotation.Roll)}`);
    serial.writeLine(`Sensors.compass ${input.compassHeading()}`);
    serial.writeLine(`Sensors.magX ${input.magneticForce(Dimension.X)}`);
    serial.writeLine(`Sensors.magY ${input.magneticForce(Dimension.Y)}`);
    serial.writeLine(`Sensors.magZ ${input.magneticForce(Dimension.Z)}`);
    basic.pause(1000);
});

// Screen input loop
while (true) {
    let bufferedText = serial.readLine(); // Be sure to write line on PC!

    if (bufferedText != "") {
        // I would use RegExp, but microbit doesn't support it apparently
        if (bufferedText.includes("Display.matrix ")) Display.matrix(bufferedText.substr(15));
        else if (bufferedText.includes("Display.text ")) Display.text(bufferedText.substr(13));
        else if (bufferedText.includes("Display.clear")) Display.clear();
    }

    basic.pause(16) // Pasue for 16ms for an ~60hz refresh rate
}