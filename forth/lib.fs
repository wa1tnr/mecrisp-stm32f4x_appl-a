\ Tue 14 Feb 18:52:39 UTC 2023

\ PROVING phase - demo each lib word for proper operation

\ STATUS: all proven 18:50z

\ PC13 is Black Pill LED - it is blue

\ Pretty sure the LED circuit negates 'normal' logic.  TODO: verify

hex 1 \ the '1' just pads the stack one element.

: <1?  ( n -- BOOL )
  dup 1 - 0< if drop -1 exit then drop 0 ;

: << ( n shifts -- shifted ) \ : GPIOCEN ( -- n ) 1 2 << ; ( gives '4' )
  lshift ;

: 2^ ( n -- 2^n ) \ 2^0 = 1 \ 2^1 = 2 \ 2^2 = 4
  dup <1?  0< if
    drop 1 exit
  then
  1 swap << ;

\ 0 2^ . 1  ok.
\ 1 2^ . 2  ok.
\ 2 2^ . 4  ok.
\ 3 2^ . 8  ok.

: delay ( n -- ) \ confirmed drops TOS no side-effects seen
  depth 1 - 0< if exit then
  \ 3 max - make sure it's 3 or more; later: test for 2, 1 and possibly 0 ;)
  1 max \ 1 seems enough for a do .. loop but zero is no good; 2 is of course just fine.

  \ zero: no good, never returns

  \ -1 max makes -17 into -1
  \ -1 max keeps   2 as 2
  \ 1992 max keeps 1992 as 1992 .. &c.

  0 do 3 0 do 11 0 do 100 0 do
      1 drop
  loop loop loop loop ;

\ 14 Feb 2023 18:52z
\ END.
