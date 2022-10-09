<CsoundSynthesizer>
<CsOptions>
-n -d 
</CsOptions>

sr=44100 ; Sample Rate
kr=22050 ; Control Rate
ksmps=2  ; sr/kr As far as I know this is always the case
nchnls=2 ; 1=mono, 2=stereo, 4=quad

<CsInstruments>
instr  1                     ; Instrument 1 begins here
kfreq chnget "Frequency"     ; Frequency
kamp chnget "Amplitude"      ; Amplitude            
itabl1 =      2                   ; Waveform Table
aout   oscil  kamp*40000, kfreq, itabl1    ; An oscillator
       outs   aout, aout            ; Output the results to a stereo sound file
       endin                        ; Instrument 1 ends here
</CsInstruments>

<CsScore>
; SCORE
f1 0 16384 10 1                                          ; Sine
f2 0 16384 10 1 0.5 0.3 0.25 0.2 0.167 0.14 0.125 .111   ; Sawtooth
f3 0 16384 10 1 0   0.3 0    0.2 0     0.14 0     .111   ; Square
f4 0 16384 10 1 1   1   1    0.7 0.5   0.3  0.1          ; Pulse
f5 0 16384 10 1 0.3 0.05 0.1 0.01                        ; Custom

;Instrument#(p1) Start(p2) Duration(p3) Amplitude(p4) Frequency(p5) Table(p6)
i1               0         3600

</CsScore>
</CsoundSynthesizer>