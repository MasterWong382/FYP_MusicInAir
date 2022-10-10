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
kc1 = 5
kvdepth = .01
kvrate = 6
kamp chnget "Amplitude"
kc2  line 5, p3, p4
asig fmpercfl .5*kamp, kfreq, kc1, kc2, kvdepth, kvrate
     outs asig, asig
endin


</CsInstruments>
<CsScore>
; sine wave.
f 1 0 32768 10 1

i1 0 100 1

e

</CsScore>
</CsoundSynthesizer>