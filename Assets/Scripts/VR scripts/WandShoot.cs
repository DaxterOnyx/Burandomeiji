using UnityEngine;

enum Element
{
	Electricity,
	Fire,
	Ice,
	Wind
}

public class WandShoot : WeaponScript
{
	public new GameObject ActiveProjectile {
		get
		{
			switch (m_element)
			{
				case Element.Electricity:
					return ElectricityProjectile;
				case Element.Fire:
					return FireProjectile;
				case Element.Ice:
					return IceProjectile;
				case Element.Wind:
					return WindProjectile;
				default:
					Debug.LogError("Element ERROR");
					return FireProjectile;
			}
		}
	}

	public GameObject FireProjectile;
	public GameObject ElectricityProjectile;
	public GameObject IceProjectile;
	public GameObject WindProjectile;
	[SerializeField]
	private Element m_element = Element.Fire;

	public override void EndUse()
	{
		base.Use();
		Shoot();
	}

	private void FixedUpdate()
	{
		//TODO charge
	}

	void ChageElement(Element p_element)
	{
		m_element = p_element;
	}
}