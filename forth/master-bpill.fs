
\ PC13 is Black Pill LED - it is blue

( hex )

VARIABLE speed
55 speed c! \ speed c@ 

: <1?  ( n -- BOOL )
  dup 1 - 0< IF
    drop -1 EXIT
  THEN
  drop 0 ;

: << ( n shifts -- )
  LSHIFT ;

: 2^ ( n -- )
  dup <1?  0< IF
    drop 1 EXIT
  THEN
  1 swap << ;

: delay ( n -- )
  DEPTH 1 - 0<
  IF EXIT THEN
  FOR 3 FOR 11 FOR 100
      FOR 1 DROP NEXT
  NEXT NEXT NEXT ;

: bdelay 3 delay ; ( -- )
: bdkdel 8 delay ; ( -- )
: ldelay 122 delay ; ( -- )
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
: on BSX led! ; ( n -- )
: off BRX led! ; ( n -- )

: setupled ( -- )
  RCC! led OUTPUT led off ;

: blinks ( n -- )
  DEPTH 1 - 0<
  IF EXIT THEN
  1 - ( normalize )
  FOR
    led on
    bdelay
    led off
    bdkdel
  NEXT ;

: linit ( -- n )
  FFFFFF9D setupled
  3 blinks 
  7 FOR bdkdel NEXT
  7 FOR bdkdel NEXT
  led off 3 blinks ;

: nullemit 0 emit ;


