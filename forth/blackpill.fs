\ Tue 14 Feb 02:38:17 UTC 2023

\ PC13 is Black Pill LED - it is blue

( hex )

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



\ 14 Feb 2023 02:34z
\ END.

