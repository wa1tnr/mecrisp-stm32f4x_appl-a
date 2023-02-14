\ Tue 14 Feb 02:53:02 UTC 2023

\ PC13 is Black Pill LED - it is blue

( hex )

hex

1
VARIABLE speed
55 speed c! \ speed c@ 

: <1?  ( n -- BOOL )
  dup 1 - 0< if drop -1 exit then drop 0 ;


: << ( n shifts -- )
  lshift ;

: 2^ ( n -- )
  dup <1?  0< if
    drop 1 exit
  then
  1 swap << ;

: delay ( n -- )
  depth 1 - 0< if exit then
  0 do 3 0 do 11 0 do 100 0 do
      1 drop
  loop loop loop loop ;

: bdelay 300 delay ; ( -- ) \ was just 3
: bdkdel 800 delay ; ( -- ) \ was just 8
: ldelay 1222 delay ; ( -- ) \ was 122
: finishmsg ." done." ; ( -- )

: RCC 40023800 ; ( -- addr ) ( p. 65 )
: RCC_AHB1ENR RCC 30 + ; ( -- addr ) ( 7.3.24 )
: RCC_APB2ENR RCC 44 + ; ( -- addr ) ( table 34 p 266 )

: GPIOCEN ( -- n ) 1 2 << ; ( gives '4' )
( 6.3.10 p.180 Rev 18 datasheet)

: RCC! ( -- )
  RCC_AHB1ENR @
  GPIOCEN or
  RCC_AHB1ENR !
;

: GPIOC 40020800 ; ( -- addr )
( 2.3 p.65 )

: GPIOC_MODER GPIOC 0 + ; ( -- addr )
( offset 0x00 8.4.1 p.281 )
: GPIOC_MODER! ( n -- )
  GPIOC_MODER @
  or GPIOC_MODER ! ;

( everything correct March 2021 above this line )

: OUTPUT ( n -- )
  ( 6 max F min ) \ was C not 6
  C max E min \ D is PC13
  2 * 1 swap << ( sets bit 26 for D 13 )
  GPIOC_MODER! ;
\ HEX ok
\ D ok
\ 2 * ok
\ 1 SWAP ok
\ LSHIFT ok
\ .S  4000000  ok
\ 2 BASE ! ok
\ DUP . 100000000000000000000000000 ok
\ 100000000000000000000000000 local

\ 100 0000 0000 0000 0000 0000 0000 local

: GPIOC_BSRR ( -- addr )
  GPIOC 18 + ; ( 18 right for all GPIOx is addrs offset )

: BSX 2^ ; ( n -- n )
: BRX 10 + 2^ ; ( n -- n )

: GPIOC_BSRR!  ( n -- )
  GPIOC_BSRR ! ;

( : led 6 ; ) \ PD6 convenience selection
: led D ; \ PC13 onboard LED
: led!  GPIOC_BSRR! ; ( n -- )

: on BRX led! ; ( n -- )
: off BSX led! ; ( n -- )

: setupled ( -- )
  RCC! led OUTPUT led off ;

: blinks ( n -- )
  depth 1 - 0<
  if exit then
  1 - ( normalize )
  0 do
    led on
    bdelay
    led off
    bdkdel
  loop ;

: linit ( -- n )
  FFFFFF9D setupled
  3 blinks 
  7 0 do bdkdel loop
  7 0 do bdkdel loop
  led off 3 blinks ;

: nullemit 0 emit ;



\ 14 Feb 2023 02:34z
\ END.

