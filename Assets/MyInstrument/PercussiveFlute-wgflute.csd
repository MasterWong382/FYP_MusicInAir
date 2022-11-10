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

kfreq =440



kjet init p4			;vary air jet
iatt = 0
idetk = 0
kngain = 0.15
kvibf = 5.925
kvamp = 0.05

asig wgflute .8, kfreq, kjet, iatt, idetk, kngain, kvibf, kvamp, 1, 10
     outs asig, asig

endin
</CsInstruments>
<CsScore>
f 1 0 16384 10 1		;sine wave

 
i 1 0 1000 0.32
e
</CsScore>
</CsoundSynthesizer>


