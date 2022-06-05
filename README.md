# Microbit Usb Control

Control a BBC Microbit through the USB port over serial to run code on seperate computers.

## How to set up Microbit with precompiled binery

1. Paste `microbit.hex` from the `bin` folder onto your Microbit
2. Once the orange light on the Microbit becomes solid again, it is finished flashing
3. The Microbit will ask you to calibrate its compass, to do so tilt the Microbit to fill the screen
4. Once all the LEDs are lit, they will turn off and you are good to go

## How to set up Microbit compiling your own binary

1. Go [here](https://makecode.microbit.org/)
2. Create a new project, paste in the contents of `microbit.ts` from the `src` folder
3. Click the download button and save it to your Microbit
4. Once the orange light on the Microbit becomes solid again, it is finished flashing
5. The Microbit will ask you to calibrate its compass, to do so tilt the Microbit to fill the screen
6. Once all the LEDs are lit, they will turn off and you are good to go

## Misc info

- Microbit reads line from serial port, so always write new lines rather than relying on the buffer, since relying on the buffer can cause broken images
