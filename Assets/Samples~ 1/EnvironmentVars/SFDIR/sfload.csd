<CsoundSynthesizer>
<CsOptions>
-n -d 
</CsOptions>
<CsInstruments>

; By  Menno Knevel - 2020

sr = 48000
ksmps = 64
nchnls = 2
0dbfs  = 1

; load in two soundfonts
isf	sfload	"sf_GMbank.sf2"

sfplist isf
	
; first sf_GMbank.sf2 is loaded and assigned to start at 0 and counting up to 328
; as there are 329 presets in sf_GMbank.sf2 (0-328).

instr 1 ; play French Horn, bank 0 program 60

inum	=	69
ivel	=	100
kamp	linsegr	1, 1, 1, 10, 1
kamp	= kamp/500000						;scale amplitude
kfreq = 1					;do not change freq from sf
a1,a2	sfplay3	ivel, inum, kamp*ivel, kfreq, 60		;preset index = 60
	outs	a1, a2	
endin

	
</CsInstruments>
<CsScore>

i1 0 1000


e
</CsScore>
</CsoundSynthesizer>


