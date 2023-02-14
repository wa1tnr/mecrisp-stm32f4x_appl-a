# Mecrisp-Stellaris STM32F411
## WeAct board - Adafruit vended

**Tue 14 Feb 19:41:37 UTC 2023**

This is a basic use of mecrisp to blink the blue LED onboard
the WeAct target.

`lib.fs` fully vetted; stack effect diagrams are good.

`blackpill.fs` holds the main program -- needs some work to
verify all stack effect diagrams (no errors noticed recently).

This is a port of old work from the Lumex display project
for stm32f4xx (407 Discovery was used for that) using
Dr. C.H. Ting's eForth (v 7.20) as downloaded from forth.org
(see this author's other repository for that).

Only 'for/next' had to be ported (using 'do' and 'loop') to
get the eForth code to run. ;)

END.
