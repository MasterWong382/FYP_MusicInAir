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

kamp = p4
kfreq = 880
kc1 = p5
kc2 = p6
kvdepth = 0.005
kvrate = 6

asig fmbell kamp, kfreq, kc1, kc2, kvdepth, kvrate
     outs asig, asig
endin

instr 2

kamp chnget "Amplitude"
kfreq chnget "Frequency"
kc1 = p5
kc2 = p6
kvdepth = 0.005
kvrate = 6

asig fmbell kamp*0.2, kfreq, kc1, kc2, kvdepth, kvrate, 1, 1, 1, 1, 1,10000
     outs asig, asig
endin

</CsInstruments>
<CsScore>
; sine wave.
f 1 0 32768 10 1



i2 0 10000 .2  5 5 10000

e

</CsScore>
</CsoundSynthesizer>