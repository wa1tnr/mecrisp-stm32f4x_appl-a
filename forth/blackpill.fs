\ Tue 14 Feb 02:33:28 UTC 2023

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

\ 14 Feb 2023 02:34z
\ END.

