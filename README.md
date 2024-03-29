# Reaction Diffusion Simulator

This Unity application simulates the Gray-Scott reaction diffusion equations using a finite difference algorithm implemented over a meshgrid of nodes. 

<p align="center">
$\frac{\partial{u}}{\partial{t}} = -uv^2 + F(1-u) + D_u\Delta u$
</p>
<p align="center">
$\frac{\partial{v}}{\partial{t}} = uv^2 - (F+k)v + D_v\Delta v$
</p>

![InitialMeshgrid](/RxnDiffusion_Initial.png?raw=true)

The final pattern formed after running the finite difference simulation.

![FinalMeshgrid](/RxnDiffusion_End.png?raw=true)
