All plants share one atlas texture. The opacity (transparency) information for "cutting out" the plants is stored in the alpha channel of the tga file.



I recommend turning off "backface culling" for all meshes, but for the plants, it is required.


The other meshes share one BaseBolor texture. You can set up the two materials with the desired roughness. Set the metalness to 0. I like to add a bit of subsurface scattering to my plant materials, too.




Some meshes, like the containers with the water, are not combined so that you can assign a different material to the water (as I did in my example scene).


Everything is unwrapped and placed on the different colors of the BaseColor texture. If you would like more color variants, you can simply move the UV shells to another color of your liking using a 3D software like e.g. blender.