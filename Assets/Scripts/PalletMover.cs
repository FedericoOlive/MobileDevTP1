using UnityEngine;

public class PalletMover : ManejoPallets 
{
    public enum MoveType {
        WASD,
        Arrows
    }
    public MoveType miInput;

    public ManejoPallets Desde, Hasta;
    bool segundoCompleto = false;

    private void Update() {
        switch (miInput) {
            case MoveType.WASD:
                if (!Tenencia() && Desde.Tenencia() && Input.GetKeyDown(KeyCode.A))
                {
                    PrimerPaso();
                }
                if (Tenencia() && Input.GetKeyDown(KeyCode.S))
                {
                    SegundoPaso();
                }
                if (segundoCompleto && Tenencia() && Input.GetKeyDown(KeyCode.D))
                {
                    TercerPaso();
                }
                break;
            case MoveType.Arrows:
                if(Input.GetKeyDown(KeyCode.LeftArrow))
                    if (!Tenencia() && Desde.Tenencia())
                    {
                        FirstStep();
                    }
                if(Input.GetKeyDown(KeyCode.DownArrow))
                    if (Tenencia())
                    {
                        SecondStep();
                    }
                if(Input.GetKeyDown(KeyCode.RightArrow))
                    if (segundoCompleto && Tenencia())
                    {
                        ThirdStep();
                    }
                break;
            default:
                break;
        }
    }
    public void FirstStep() => PrimerPaso();
    public void SecondStep() => SegundoPaso();
    public void ThirdStep() => TercerPaso();
    public void PrimerPaso() 
    {
        Desde.Dar(this);
        segundoCompleto = false;
    }
    public void SegundoPaso() 
    {
        base.Pallets[0].transform.position = transform.position;
        segundoCompleto = true;
    }
    public void TercerPaso() 
    {
        Dar(Hasta);
        segundoCompleto = false;
    }
    public override void Dar(ManejoPallets receptor) 
    {
        if (Tenencia()) {
            if (receptor.Recibir(Pallets[0])) 
            {
                Pallets.RemoveAt(0);
            }
        }
    }
    public override bool Recibir(Pallet pallet) 
    {
        if (!Tenencia()) {
            pallet.Portador = this.gameObject;
            base.Recibir(pallet);
            return true;
        }
        else
            return false;
    }
}