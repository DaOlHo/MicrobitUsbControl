serial.redirectToUSB();
serial.setRxBufferSize(64);
serial.setTxBufferSize(64)

function writeScreen(input: string) {
    input = input.replaceAll(' ', '');
    basic.clearScreen();

    let stringIndex = 0;

    for (let y = 0; y < 5; y++) {
        for (let x = 0; x < 5; x++) {
            if (input.charAt(stringIndex) == "1") led.plot(x, y);

            stringIndex++;
        }
    }
}

input.onButtonPressed(Button.A, function () {
    serial.writeLine("A");
})

input.onButtonPressed(Button.B, function () {
    serial.writeLine("B");
})

input.onButtonPressed(Button.AB, function () {
    serial.writeLine("AB");
})

while (true) {
    let bufferedText = serial.readLine();

    if (bufferedText != "") {
        writeScreen(bufferedText);
    }

    basic.pause(16)
}