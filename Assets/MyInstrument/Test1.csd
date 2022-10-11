<CsoundSynthesizer>
<CsOptions>
-n -d
</CsOptions>
<CsInstruments>

sr 		= 		44100
kr 		= 		4410
ksmps 	= 		10
nchnls 	= 		1


instr   114
kfreq chnget "Frequency"
k1      linen   p4, p7, p3, p8
k2      line    p11, p3, p12
a1   	foscil 	k1, kfreq, p9, p10, k2, p6
		out     a1
		endin


		
</CsInstruments>
<CsScore>

f1  0 4096 10   1    
f2  0 4096 10   1  .5 .333 .25 .2 .166 .142 .125 .111 .1 .09 .083 .076 .071 .066 .062
f3  0 4097 20   2  1
f4  0 0    1   "sing.aif" 		0 	4 	0
f5  0 0    1   "hellorcb.aif" 	0 	4 	0 
f6  0 1024 7    0  	10   	1   1000   1   		14     	0
f7  0 1024 7	0	128		1	128		.6		512		.6		256 	0
f8  0 1024 5   .01  256   	1   192   	.5   	256  	.5   	64  	.01



;ins	strt	dur amp     frq     fn	atk		rel    fc   fm  indx1	indx2
;============================================================================
i114 	0   	100 	10000	220  	1   .1		2.9  	1   .5	1		6

e
</CsScore>
</CsoundSynthesizer>