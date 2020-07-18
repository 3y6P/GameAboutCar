using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CarShopScript : MonoBehaviour {

	[Header("Money")]
	public Text MoneyText;
	public string MoneyValue;

	[Header("Rang")]
	public Text RangText;
	public string RangValue;

	[Header("NameTransport")]
	public Text NameTransportText;
	public Text NameTransportText1;
	public Text NameTransportText2;
	public Text NameTransportText3;
	public string NameTransportValue;

	[Header("MinRang")]
	public Text MinRangText;
	public Text MinRangText1;
	public string MinRangValue;

	[Header("Speed")]
	public Text SpeedText;
	public string SpeedValue;

	[Header("Accelerate")]
	public Text AccelerateText;
	public string AccelerateValue;

	[Header("Price")]
	public Text PriceText;
	public Text PriceText1;
	public Text PriceText2;
	public Text PriceText3;
	public string PriceValue;

	[Header("Status")]
	public Text StatusText;
	public string StatusValue;

	[Header("Color")]
	public Text ColorText;
	public string ColorValue;


	// Use this for initialization
	void Start () {
		GetCarValues();
	}

	public void GetCarValues()
	{
		//Деньги игрока
		MoneyText.text = MoneyValue;

		//Ранг игрока
		RangText.text = RangValue;

		//Наименование транспорта
		NameTransportText.text = NameTransportValue;
		NameTransportText1.text = NameTransportValue;
		NameTransportText2.text = NameTransportValue;
		NameTransportText3.text = NameTransportValue;

		//Требуемый ранг
		MinRangText.text = MinRangValue;
		MinRangText1.text = MinRangValue;

		//Скорость транспорта
		SpeedText.text = SpeedValue;

		//Ускорение транспорта
		AccelerateText.text = AccelerateValue;

		//Цена транспорта
		PriceText.text = PriceValue;
		PriceText1.text = PriceValue;
		PriceText2.text = PriceValue;
		PriceText3.text = PriceValue;

		//Статус транспорта (Куплен/Не куплен)
		StatusText.text = StatusValue;

		//Цвет транспорта
		ColorText.text = ColorValue; ;
	}
	
	void Update () {
		
	}
}
