using UnityEngine;
using System.Collections;

public class Cinta : ManejoPallets 
{
	public bool Encendida;//lo que hace la animacion
	bool ConPallet = false;
	public float Velocidad = 1;
	public GameObject Mano;
	public float Tiempo = 0.5f;
	float Tempo = 0;
	Transform ObjAct = null;
	
	//animacion de parpadeo
	public float Intervalo = 0.7f;
	public float Permanencia = 0.2f;
	float AnimTempo = 0;
	public Color32 ColorParpadeo;
	Color32 ColorOrigModel;
	
	void Update () 
	{
		//animacion de parpadeo
		if(Encendida)
		{
			AnimTempo += Time.deltaTime;
        }
		
		//movimiento del pallet
		for(int i = 0; i < Pallets.Count; i++)
		{
			if(Pallets[i].GetComponent<Renderer>().enabled)
			{
				if(!Pallets[i].GetComponent<Pallet>().EnSmoot)
				{
					Pallets[i].GetComponent<Pallet>().enabled = false;
					Pallets[i].TempoEnCinta += Time.deltaTime;
					
					Pallets[i].transform.position += transform.right * Velocidad * Time.deltaTime;
					Vector3 vAux = Pallets[i].transform.localPosition;
					vAux.y = 3.61f;//altura especifica
					Pallets[i].transform.localPosition = vAux;					
					
					if(Pallets[i].TempoEnCinta >= Pallets[i].TiempEnCinta)
					{
						Pallets[i].TempoEnCinta = 0;
						ObjAct.gameObject.SetActiveRecursively(false);
					}
				}
			}
		}
	}
	
	void OnTriggerEnter(Collider other)
	{
		ManejoPallets recept = other.GetComponent<ManejoPallets>();
		if(recept != null)
		{
			Dar(recept);
		}
	}
	
	
	//------------------------------------------------------------//

	public override bool Recibir(Pallet p)
	{
        Tempo = 0;
        Controlador.LlegadaPallet(p);
        p.Portador = this.gameObject;
        ConPallet = true;
        ObjAct = p.transform;
        base.Recibir(p);
        //p.GetComponent<Pallet>().enabled = false;
        Apagar();

        return true;
    }
	
	public void Encender()
	{
		Encendida = true;
	}
	public void Apagar()
	{
		Encendida = false;
		ConPallet = false;
	}
}
