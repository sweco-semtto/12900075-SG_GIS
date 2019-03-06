
; '*' means the calculations are performed relative to margins of a print area
; Without '*' calculations will be done relative to the page border.
; For PDF output margins are always equal to the page size.

[TatukGIS PrintTemplate]
; PAGESIZE can be set only on PDF device
  PAGESIZE=297.00mm,210.00mm

; place graphic and text at the top
;  GRAPHIC1= *1.00cm, *0.75cm, *2.52cm, *2.47cm,"logo.horn.gif"

; place text at the top
  TEXT1=    *5.00cm, *0.75cm,*-0.75cm, *3.00cm,RIGHTJUSTIFY,NAVY,,38,BOLD:ITALIC

; place text at the top
  TEXT2=    *0.75cm,*-0.50cm,*-0.75cm,*-0.02cm,RIGHTJUSTIFY,BLACK,,8

; draw backround border for the map & the map itself
  BOX1=     *0.75cm, *0.75cm,*-0.75cm,*-0.75cm,Green
;  BOX2=     *0.85cm, *.10cm,*-6.85cm,*-0.85cm,Yellow
  MAP1=     *0.90cm, *0.90cm,*-0.90cm,*-0.90cm

; draw the scale
  Scale1=   *-10.00cm, *-2cm,*-7.00cm,*-1.00cm


; draw background border for the legend & the legend itself
;  BOX3=    *-6.50cm, *3.00cm,*-0.75cm,*-0.75cm,Blue
;  BOX4=    *-6.40cm, *3.10cm,*-0.85cm,*-0.85cm,Yellow
  ;white background because legend is transparent by default
;  BOX5=    *-6.25cm, *3.25cm,*-1.00cm,*-1.00cm,White
;  LEGEND1= *-6.15cm, *3.30cm,*-1.00cm,*-1.00cm

; draw thin line around the map
  FRAME1=   *0.01cm, *0.01cm,*-0.01cm,*-0.01cm,BLACK,0.01mm
