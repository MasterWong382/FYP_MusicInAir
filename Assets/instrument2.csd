<CsoundSynthesizer>
<CsOptions>
-n -d
</CsOptions>
<CsInstruments>

sr = 44100 
ksmps = 32 
nchnls = 2 
0dbfs  = 1 

instr 1

ifn  = p4
kfreq chnget "Frequency"
asig oscil .8, kfreq, ifn
     outs asig,asig
    
endin
</CsInstruments>
<CsScore>
f 1 0 16384 11 1 1	;number of harmonics = 1
f 2 0 16384 11 2 1	;number of harmonics = 1
f 3 0 16384 11 3 1	;number of harmonics = 1
f 4 0 16384 11 4 1	;number of harmonics = 1
f 5 0 16384 11 5 1	;number of harmonics = 1
f 6 0 16384 11 10 1 .7	;number of harmonics = 10
f 7 0 16384 11 10 5 2	;number of harmonics = 10, 5th harmonic is amplified 2 times


i1 0 1000 3

e
</CsScore>
</CsoundSynthesizer>