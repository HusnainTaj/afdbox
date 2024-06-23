# afdbox
A portable program to quickly set up a Windows PC to run assembly programs.

https://github.com/HusnainTaj/afdbox/assets/85726252/434f1942-cd94-4fc2-b505-3cc066ace0da

### Online VS Code Editor: https://vscode.dev

## What?
A program that allows you to Compile (with NASM), Run (with DOSBox) and Debug (with AFD) .asm files by just double clicking on them.

## Why?
I'm Lazy.

## How?

1. Download and extract the `labsetup.zip` from the [Releases Page](https://github.com/HusnainTaj/afdbox/releases).
2. Run the `afdbox.exe` once
3. Right-Click on any `.asm` file and select `afdbox.exe` in the `Open With` context menu

## To always open `.asm` files using `afdbox.exe`
1. Right click on a `.asm` file and open its Properties
2. Click on the "Change" Button next to the "Opens with" text
3. Select `afdbox.exe` 

## Modes
### Debug
In this mode, the program will be opened in AFD after it is compiled. This mode is enabled by default.

### Run
This mode will just execute the program after compilation. This is useful when the program prints something on the screen.

### Changing Modes
Open `afdbox.exe` and press any key to switch between `Run` and `Debug` Mode. 

<hr/>

> [!note]
> If you have moved `labsetup` folder after running `afdbox.exe` once, you will have to re-run `afdbox.exe` for the program to work again.
