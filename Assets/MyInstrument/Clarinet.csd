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

kfreq chnget "Frequency"
kamp chnget "Amplitude"
if kfreq <=10 then 
    kfreq = 220
endif

kstiff = -0.3
iatt = 0.1
idetk = 0.1
kngain init p4		;vary breath
kvibf = 5.735
kvamp = 0.1


asig wgclar .5*kamp, kfreq, kstiff, iatt, idetk, kngain, kvibf, kvamp, 1
     outs asig, asig
      
endin
</CsInstruments>
<CsScore>
f 1 0 16384 10 0.6	;sine wave


i1 0 100 2		;more breath
e
</CsScore>
</CsoundSynthesizer>