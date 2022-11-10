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
kc1 = p4
kc2 = p5
kvrate = 6

kvdpth line 0, p3, p6
asig   fmb3 .1*kamp, kfreq, kc1, kc2, kvdpth, kvrate
       outs asig, asig

endin
</CsInstruments>
<CsScore>
;sine wave.
f 1 0 32768 10 1

i 1 0 100  5  5 0.1
e
</CsScore>
</CsoundSynthsizer>