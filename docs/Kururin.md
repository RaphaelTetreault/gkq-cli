Kururin

* obj
  * 2D
    * TXG files - texture GameCube?
      * 0x00 file count
      * 0x04 descriptor ptr
      * 0x08 24 bytes padding
      * 0x20 - 4 bytes count? true/false?
      * 0x24 - 4 bytes text format
      * 0x28 unk
      * 0x2C width?
      * 0x30 height?
      * 0x34 unk
      * 0x38 ptr to data
      * 0x3C padding? FFFFFFFF
  * Area
    * Some kind of scene data (as mention in files at the end!)
  * W-X (A, B, C, D, E)
    * DAT files - stage data?
    * 0x00 file length
    * 0x04 ptr, subtract some amount? Or is that data different?
    * 0x08 count? - NOT object count, eg. coins, start, heal, end...
    * 0x0C count? bool?
    * 0x10 - 0x 1C zeros?
* Procs
  * Seems like coordinates? sets of 2 or 4 floats?
  * Ends in FF x16
* Tehon
  * "Model" ?
  * rps - texture?
  * rpt - texture?





c_01.dat

* 0x37a0 9800 gx command
* 0x37a2 reverse order bytes for length
* Stride: 8 bytes, appears to be fixed values
* Some sort of "TevLayer" bytes before, seems to be 0x20 sized structs
  * Seems like it points back to previously defined, actual TevLayers types
  * Maybe it builds out all textures, the tev, then dynamically links back to it?



c_06.dat

* 0x20 byte header
  * 
* 0x0020 graphics data?
* 0x1800 graphics data?
* 0x2d40 GC graphics data 9800 command
* 



Remakr: values after 2nd large binary block: struct size 5x 4 bytes. Address sprinkled between. eg. Addr jumps back something like 0x30 bytes, has 5 values, then is proceeded by another value that repeats.



Lighting data in stage bg dat (test models make me assume so)

files/frame == collision (triangles?)

files/procs == object movement xyz + rot z?

TODO: are WX-em the object placement scripts?

DAT: "scene object"? Got mesh, tex, and other stuff