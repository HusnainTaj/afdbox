# afdbox

## What?
A program that allows you to Compile (with NASM), Run (with DOSBox) and Debug (with AFD) .asm files by just double clicking on them.

## Why?
I'm Lazy

## How?
Download and install the latest binary from the [Releases Page](https://github.com/HusnainTaj/afdbox/releases).

1. Run the `afdbox.exe`
2. Specify the Path to NASM, DOSBox and AFD exe's
3. Close `afdbox.exe`
4. Double click any `.asm` file and it will open using `afdbox.exe`. If not, Right-Click and select `afdbox.exe` in the `Open With` context menu.

### To always open `.asm` files using `afdbox.exe`
1. Right click a `.asm` file and open its Properties.
2. Click on "Change" Button next to the "Opens with" text
3. Select the `afdbox.exe` 

## Dependencies
- NASM
- DOSBox
- AFD

> None of these are included in `afdbox.exe` and must be installed by you separately. [Download here](https://github.com/HusnainTaj/afdbox/releases/download/main/COAL-Setup.zip)

## Note
- If you have moved any of the dependencies, you will have to re-enter their paths. You can do so by resetting `afdbox.exe` by directly running it and entering `r` command.