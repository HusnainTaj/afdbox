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
4. Double click any `.asm` file and it will open using `afdbox.exe`. If not Right-Click and select `afdbox.exe` in the `Open With` context menu.

## Dependencies
- NASM
- DOSBox
- AFD

> None of these are included in `afdbox.exe` and must be installed by you separately.

## Note
- You can factory reset `afdbox.exe` by directly running it and entering `reset` command
- If your program (.asm) has errors and can't be compiled, DOSBox will not launch and nothing will be shown. So make sure your program is compilable.