\ Tue 14 Feb 23:56:14 UTC 2023

hex 1 \ pad

: <1?  ( n -- BOOL )
  dup 1 - 0< if drop -1 exit then drop 0 ;

\ 1 2 << ( gives '4' )

: << ( n shifts -- shifted )
  lshift ;

\ 2^0 = 1 \ 2^1 = 2 \ 2^2 = 4

: 2^ ( n -- 2^n )
  dup <1?  0< if
    drop 1 exit
  then
  1 swap << ;

: _mdelay ( n -- ) \ milliseconds to delay - unit time
  $A2D 0 do 1 drop loop
;

: delay ( n -- )
  depth 1 - 0< if exit then
  1 max
  0 do
    _mdelay
  loop
;

\ 14 Feb 2023 23:56z
\ END.
