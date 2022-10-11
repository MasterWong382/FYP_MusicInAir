<CsoundSynthesizer>
<CsOptions>
-n -d
</CsOptions>
<CsInstruments>

sr 		= 		44100
kr 		= 		4410
ksmps 	= 		10
nchnls 	= 		1

		instr  	113
k1		linen	p4, p7, p3, p8
a1   	oscil	k1, p5, p6
       	out  	a1
		endin

		instr   114
k1      linen   p4, p7, p3, p8
k2      line    p11, p3, p12
a1   	foscil 	k1, p5, p9, p10, k2, p6
		out     a1
		endin

		instr   115
k1      linen   p4, p7, p3, p8
k2      expon  	p9, p3, p10	
a1   	buzz   	1, p5, k2+1, p6
        out     a1*k1
		endin

		instr   116
k1      linen   p4, p7, p3, p8
k2      linseg  p5, p3/2, p10, p3/2, p5	
a1 		pluck 	k1, k2, p5, p6, p9
		out 	a1
		endin

		instr 	117
k2      linseg  p5, p3/2, p9, p3/2, p5
k3      line    p10, p3, p11
k4      line    p12, p3, p13
k5      expon   p14, p3, p15
k6      expon	p16, p3, p17
a1 		grain 	p4, k2, k3, k4, k5, k6, 1, p6, 1
a2      linen   a1, p7, p3, p8
		out 	a2
		endin
				
		instr 	118					
k1      oscil   p4, 1/p3, p7
k2      expseg  p5, p3/3, p8, p3/3, p9, p3/3, p5
a1 		loscil  k1, k2, p6
		out 	a1
		endin
		
		instr   119
kfreq chnget "Frequency"
k1      oscil   p4, 1/p3 * p8, p7
k2      line    p11, p3, p12
a1   	foscil 	k1, kfreq, p9, p10, k2, p6
		out     a1
		endin

		
</CsInstruments>
<CsScore>

;Function 1 uses the GEN10 subroutine to compute a sine wave
;Function 2 uses the GEN10 subroutine to compute the first sixteen partials of a sawtooth wave
;Function 3 uses the GEN20 subroutine to compute a Hanning window for use as a grain envelope
;Function 4 uses the GEN01 subroutine to read in an AIF audio file
;Function 5 uses the GEN01 subroutine to read in annother AIF audio file
;Function 6 uses the GEN07 subroutine to compute a linear AR "GATE" envelope function
;Function 7 uses the GEN07 subroutine to compute a linear ADSR envelope function
;Function 8 uses the GEN05 subroutine to compute an exponential ADSR envelope function

f1  0 4096 10   1    
f2  0 4096 10   1  .5 .333 .25 .2 .166 .142 .125 .111 .1 .09 .083 .076 .071 .066 .062
f3  0 4097 20   2  1
f4  0 0    1   "sing.aif" 		0 	4 	0
f5  0 0    1   "hellorcb.aif" 	0 	4 	0 
f6  0 1024 7    0  	10   	1   1000   1   		14     	0
f7  0 1024 7	0	128		1	128		.6		512		.6		256 	0
f8  0 1024 5   .01  256   	1   192   	.5   	256  	.5   	64  	.01





;ins	strt	dur   amp     frq     fn	atk		rel		bend	dens1	dens2	ampof1	ampof2	pchof1	pchof2	gdur1	gdur2
;=============================================================================================================================
i119		0	100	  10000	440  		



e
</CsScore>
</CsoundSynthesizer>