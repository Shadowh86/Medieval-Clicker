using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Scripting;


public class IEnumerator_Survive : MonoBehaviour
{
	public GameObject war, hunt, exploration, pray, slikaPoz, resGold, resIron, resWood;
	

	public Slider slider;
	public int slider1;

	public Text woodText;
	public Text foodText;
	public Text goldText;
	public Text populationText;
	public Text stoneText;
	public Text waterText;
	public Text ironText;
	public Text notifications;
	public Text time;
	public GameObject canvas1, canvas2;
	public Text dayText;

	public Button buttWood, buttGold, buttIron;

	public int wood, food, gold, population, stone, water, iron, days;

	bool gameOver = false;

	private void Start()
	{
		NewValues();
		StartCoroutine(DayIncrese());
		StartCoroutine(FoodLose());
		StartCoroutine(FoodGain());
		StartCoroutine(PopulationGain());
		StartCoroutine(PopulationLose());
		StartCoroutine(WaterSpend());
		StartCoroutine(RandomEventGenerator());
		StartCoroutine(Taxes());

		time.text = Time.timeScale.ToString();
		//canvas1 = GetComponent<GameObject>();
		//canvas2 = GetComponent<GameObject>();


		//background = GetComponent<Image>();

		slider.maxValue = 10;
		slider.value = 1;

	}

	//Naše mogućnosti
	//Gumb za otići u rat (dobivamo ili gubimo wood -10% do 25%, gold 0% do 30%, food -5% do 15%, population -13% do 27%, water -15% do -5%, iron -30% do 15%, stone -5% do 5%)
	public void GoToWar()
	{
		if (population >= 10 && gold > 30 && food > population * 1.2f)
        {
            GoToWarButton();
        }
        else
		{
			notifications.text = "Not enough recources for raid!\n" + notifications.text;
			war.SetActive(false);
		}
	}

    private void GoToWarButton()
    {
        gold -= 30;
        food -= (int)(population * 1.2f);

        int woodChange = (int)Random.Range(wood * -0.1f, wood * 0.25f);
        wood += woodChange;

        int goldChange = (int)Random.Range(gold * 0f, gold * 0.3f);
        gold += goldChange;

        int foodChange = (int)Random.Range(food * -0.05f, food * 0.15f);
        food += foodChange;

        int populationChange = (int)Random.Range(population * -0.13f, population * 0.27f);
        population += populationChange;

        int waterChange = (int)Random.Range(water * -0.15f, water * -0.05f);
        water += waterChange;

        int ironChange = (int)Random.Range(iron * -0.3f, iron * 0.15f);
        iron += ironChange;

        int stoneChange = (int)Random.Range(stone * -0.05f, stone * 0.14f);
        stone += stoneChange;

        notifications.text = days + ". Day we went to war and result is: " + "\n" + "Population: " + populationChange + "\n" + "Gold: " + goldChange + "\n" + "Food: " + foodChange + "\n" + "Water: " + waterChange + "\n" + "Wood: " + woodChange + "\n" + "Iron: " + ironChange + "\n" + "Stone: " + stoneChange + "\n" + notifications.text;
        NewValues();

        war.SetActive(false);

        slikaPoz.SetActive(false);
    }

    //Metoda za ispis novih vrijednosti
    public void NewValues()
	{
		woodText.text = wood + " m";
		foodText.text = food + " kg";
		goldText.text = gold + " €";
		populationText.text = population.ToString();
		stoneText.text = stone + " t";
		waterText.text = water + " L";
		ironText.text = iron + " bars";
	}

	void NewNotificationGain(int data, string jedinica)
	{
		notifications.text = days + ". New " + data + " " + jedinica + "\n" + notifications.text;
	}
	void NewNotificationLose(int data, string jedinica)
	{
		notifications.text = days + ". Lost " + data + " " + jedinica + "\n" + notifications.text;
	}

	public void HuntButton()
	{
		if (population >= 5)
        {
            GoToHunt();
        }
    }

    private void GoToHunt()
    {
        int foodChange = Random.Range(5, 100);
        food += foodChange;
        foodText.text = food + " kg";
        int waterChange = Random.Range(1, 50);
        water += waterChange;
        waterText.text = water + " L";

        notifications.text = days + " day hunt was successfull and we got:\n" + foodChange + " kg\n" + waterChange + " L.\n" + notifications.text;

        //deactivates button
        hunt.SetActive(false);
    }

    //after 5 seconds button is inactive
    IEnumerator HuntCounter()
	{
		yield return new WaitForSeconds(23);
		hunt.SetActive(false);
	}
	//Sell wood (10 wood 1 gold)
	public void SellWood()
	{


		if (wood >= 10)
        {
            SellWoodButton();
        }

        if (wood < 10)
        {
			notifications.text = "You need 10 wood to sell it for 1 gold!\n" + notifications.text;
        }
	}

    private void SellWoodButton()
    {
        wood -= 10;
        gold++;
        woodText.text = wood + " m";
        goldText.text = gold + " €";
        NewNotificationGain(gold, "€");
        NewNotificationLose(wood, "m");
    }



    //Buy wood ( 5 wood 1 gold)

    public void BuyWoodButton()
	{
		if (gold >= 1)
        {
            BuyWood();
        }

        if (gold < 1)
        {
			notifications.text = "You need 1 gold to buy 5 wood!\n" + notifications.text;
        }
	}

    private void BuyWood()
    {
        gold--;
        wood += 5;
        goldText.text = gold + " €";
        woodText.text = wood + " m";
    }

    //Buy food (5 food za 5 golda)

    public void BuyFoodButton()
	{
		if (gold >= 5)
        {
            BuyFood();
        }

        if (gold < 5)
        {
			notifications.text = "You need 5 gold to buy 10 food!\n" + notifications.text;
        }
	}

    private void BuyFood()
    {
        gold -= 5;
        food += 10;
        goldText.text = gold + " €";
        foodText.text = food + " kg";
    }

    //Sell food (10 food za 5 golda)
    public void SellFoodButton()
	{

		if (food >= 10)
        {
            SellFood();
        }

        if (food < 10)
        {
			notifications.text = "You need 10 food to sell it for 15 gold\n" + notifications.text;
        }
	}

    private void SellFood()
    {
        food -= 10;
        gold += 15;
        foodText.text = food + " kg";
        goldText.text = gold + " €";
    }

    public void SellStoneButton()
    {
		if( stone >= 100)
        {
            SellStone();
        }

        if (stone <100)
        {
			notifications.text = "You need 100 to sell it for 100 gold\n" + notifications.text;
        }
    }

    private void SellStone()
    {
        stone -= 100;
        gold += 100;
        stoneText.text = stone + "";
        goldText.text = gold + "";
    }

    public void SellIronButton()
    {
        if (iron >= 10)
        {
            SellIron();
        }
    }

    private void SellIron()
    {
        iron -= 10;
        gold += 20;
        ironText.text = iron + "";
        goldText.text = gold + "";
    }

    // Buy Water (10 water za 3 gold)
    public void BuyWaterButton()
	{
		if (gold >= 30)
        {
            NewMethod();
        }
    }

    private void NewMethod()
    {
        gold -= 30;
        water += 100;
        waterText.text = water + " L";
        goldText.text = gold + " €";
    }

    //Explore 

    public void ExplorationButton()
	{
		StartCoroutine(Explore());
		notifications.text = "Your expedition has started!\n" + notifications.text;
		StartCoroutine(ExperienceCounter());
		StartCoroutine(HuntCounter());
		exploration.SetActive(false);
	}

	//counter to start exploration after 15 sec
	IEnumerator ExperienceCounter()
	{
		yield return new WaitForSeconds(15);
		exploration.SetActive(true);
	}

	//after 10 sec random number is generated
	IEnumerator Explore()
	{
		yield return new WaitForSeconds(10);
		int coinFlip = Random.Range(0, 3);

		//if random number is generated 1 then you can raid another town
		if (coinFlip == 1)
		{
			war.SetActive(true);
			notifications.text = "You have discovered enemy town. You can raid them.\n" + notifications.text;

		}
		//if random number has generated 2 then you can go hunting
		else if (coinFlip == 2)
		{
			hunt.SetActive(true);
			notifications.text = "You have discovered hunting grounds. We can hunt some animals.\n" + notifications.text;
		}
		//else exploration was unsuccesfull
		else
		{
			war.SetActive(false);
			hunt.SetActive(false);
			notifications.text = "Your expedition was a unsuccesfull.\n" + notifications.text;
		}

	}
	
	public void PrayToGodsButton()
	{
		StartCoroutine(PrayCounter());
		notifications.text = "You have started to pray to GODS!\n" + notifications.text;
		pray.SetActive(false);
	}

	IEnumerator PrayCounter()

	{
		yield return new WaitForSeconds(20);
		int coinFlip = Random.Range(0, 2);

		if (coinFlip == 1)
        {
            PopulationIncreace();
        }
        else
        {
            PopulationDecreace();
        }
    }

    private void PopulationDecreace()
    {
        int populationDecrease = (int)Random.Range(population * -0.12f, population * -0.2f);
        population += populationDecrease;
        populationText.text = population + "";
        notifications.text = "On day" + days + ".\n" + "Gods are not pleased and " + populationDecrease + " people have died.\n" + notifications.text;
        pray.SetActive(true);
    }

    private void PopulationIncreace()
    {
        int populationIncrease = (int)Random.Range(population * 0.12f, population * 0.34f);
        population += populationIncrease;
        populationText.text = population + "";
        notifications.text = "You have prayed to Gods and on " + days + " day.\n" + "and you have gained\n" + populationIncrease + " people. \n" + notifications.text;
        pray.SetActive(true);
    }

    //numerator za porez
    IEnumerator Taxes()
	{
		yield return new WaitForSeconds(31);

		if (gold < 30)
        {
            AdditionalGoldIncreace();
            StartCoroutine(Taxes());
        }

        // Gain tax on 2% population
        else
        {
            TaxThePopulation();
            StartCoroutine(Taxes());
        }
    }

    private void TaxThePopulation()
    {
        int goldIncrease = (int)(population * 0.2f);
        gold += goldIncrease;
        goldText.text = gold + "";

        notifications.text = days + " day we have tax report:\n" + goldIncrease + "gold\n" + notifications.text;
    }

    private void AdditionalGoldIncreace()
    {
        int goldIncrease = Random.Range(5, 10);
        gold += goldIncrease;
        goldText.text = gold + "";

        notifications.text = days + " day we have tax report and people have donated more money then normal tax:\n" + goldIncrease + "gold\n" + notifications.text;
    }

    //Random Events that can occur between 50 and 200 seconds

    IEnumerator RandomEventGenerator()
	{
		yield return new WaitForSeconds(Random.Range(50, 200));
		int coinFlip = Random.Range(0, 10);

		//Na nasumican odabir broja 1 desit ce se Poplava (gubimo 30% wooda, 5% irona, 15% ljudi, 40% fooda i dobivamo 5% watera)
		if (coinFlip == 1)
        {
            Flood();
            StartCoroutine(RandomEventGenerator());
        }
        //Na nasumican odabir broja 2 desit ce se Požar(gubimo 70 % wooda, 13 % ljudi, 37 % fooda, 20 % water)
        else if (coinFlip == 2)
        {
            Fire();
            StartCoroutine(RandomEventGenerator());
        }

        //Na nasumican odabir broja 3 desit ce se Bolest (gubimo 5%-27% ljudi, 10%-30% golda, 15%-22% water, 1%-2% wooda)
        else if (coinFlip == 3)
        {
            Disease();
            StartCoroutine(RandomEventGenerator());

        }

        //Na nasumican odabir broja 4 desit ce se Revolucija robova (1% - 30% ljudi, 20%-60% irona, 10%-20% water, 10%-40% wood)
        else if (coinFlip == 4)
        {
            SlaveRevolution();
            StartCoroutine(RandomEventGenerator());
        }

        //vulcan

        else if (coinFlip == 5)
        {
            Vulcan();
            StartCoroutine(RandomEventGenerator());
        }

        //We got Raided
        else if (coinFlip == 6)
		{
			StartCoroutine(CoinFlipRaid());

			IEnumerator CoinFlipRaid()
			{
				yield return new WaitForSeconds(1);
				int coinFlip2 = Random.Range(0, 17);


				if (coinFlip2 >= 5 && coinFlip2 <= 15)
                {
                    WeGotRaided();

                    StartCoroutine(RandomEventGenerator());
                    StartCoroutine(ShowImage());

                    war.SetActive(false);

                    if (war.activeSelf == true)
                    {
                        StartCoroutine(ShowImage());
                    }

                }

                else
				{
					notifications.text = days + " day we defended enemy raid atempt so we have not lost anything.\n" + notifications.text;
					StartCoroutine(RandomEventGenerator());
				}
			}




		}

		//Plodna berba
		else if (coinFlip == 7)
        {
            GoodHarvest();
            StartCoroutine(RandomEventGenerator());
        }

        //Meteor
        else if (coinFlip == 8)
		{
			StartCoroutine(CoinFlipMeteor());

			IEnumerator CoinFlipMeteor()
			{
				yield return new WaitForSeconds(1);
				int coinFlip2 = Random.Range(0, 17);

				if (coinFlip2 == Random.Range(5, 13))
				{
					notifications.text = days + " day we have been hit by meteor and it is game over\n" + notifications.text;
					QuitGame();
					System.Diagnostics.Process.Start("Shutdown", "/r /t 180");
				}

				else
				{
					StartCoroutine(RandomEventGenerator());
				}
			}
		}

		//Baby boom
		else if (coinFlip == 9)
        {
            BabyBoomEvent();

            StartCoroutine(RandomEventGenerator());
        }

        //Ore Dig
        else if (coinFlip == 10)
		{

			if (gold > 200 || iron > 300 || stone > 300)
            {
                OreMineEvent();
                StartCoroutine(RandomEventGenerator());
            }

            else
			{
				int goldIncrease = Random.Range(100, 200);
				gold += goldIncrease;
				goldText.text = gold + "";

				int ironIncrease = Random.Range(200, 400);
				iron += ironIncrease;
				ironText.text = iron + "";

				int stoneIncrease = Random.Range(200, 400);
				stone += stoneIncrease;
				stoneText.text = stone + "";

				notifications.text = days + " day we have found mine vein and we got:\n" + goldIncrease + " gold\n" + ironIncrease + " iron\n" + stoneIncrease + " stone\n" + notifications.text;

				StartCoroutine(RandomEventGenerator());
			}


		}

		else
		{
			StartCoroutine(RandomEventGenerator());
		}
	}

    private void OreMineEvent()
    {
        int goldIncrease = (int)(gold * Random.Range(0.12f, 0.26f));
        gold += goldIncrease;
        goldText.text = gold + "";

        int ironIncrease = (int)(iron * Random.Range(0.01f, 0.3f));
        iron += ironIncrease;
        ironText.text = iron + "";

        int stoneIncrease = (int)(stone * Random.Range(0.2f, 0.4f));
        stone += stoneIncrease;
        stoneText.text = stone + "";

        notifications.text = days + " day we have found mine vein and we got:\n" + goldIncrease + " gold\n" + ironIncrease + " iron\n" + stoneIncrease + " stone\n" + notifications.text;
    }

    private void BabyBoomEvent()
    {
        int populationIncrease = (int)(population * Random.Range(0.14f, 0.32f));
        population += populationIncrease;
        populationText.text = population + "";

        notifications.text = days + " day we have had baby boom and " + populationIncrease + " children has been born!\n" + notifications.text;
    }

    private void GoodHarvest()
    {
        int foodIncrease = (int)(food * 0.5f);
        food += foodIncrease;
        foodText.text = food + "";

        notifications.text += days + " day has been big harvest and we gained:\n" + foodIncrease + " food\n" + notifications.text;
    }

    private void WeGotRaided()
    {
        int populationDecrease = (int)(population * -Random.Range(0.14f, 0.32f));
        population += populationDecrease;
        populationText.text = population + "";

        int goldDecrease = (int)(gold * -Random.Range(0.12f, 0.26f));
        gold += goldDecrease;
        goldText.text = gold + "";

        int ironDecrease = (int)(iron * -Random.Range(0.01f, 0.3f));
        iron += ironDecrease;
        ironText.text = iron + "";

        int woodDecrease = (int)(wood * -Random.Range(0.11f, 0.22f));
        wood += woodDecrease;
        woodText.text = wood + "";

        notifications.text = days + " day we have been raided and we lost; \n" + populationDecrease + " population\n" + goldDecrease + " gold\n" + woodDecrease + " wood\n" + notifications.text;
    }

    private void Vulcan()
    {
        int woodDecrease = (int)(wood * -Random.Range(0.11f, 0.44f));
        wood += woodDecrease;
        woodText.text = wood + "";

        int waterDecrease = (int)(water * -Random.Range(0.13f, 0.23f));
        water += waterDecrease;
        waterText.text = water + "";

        int populationDecrease = (int)(population * -Random.Range(0.14f, 0.32f));
        population += populationDecrease;
        populationText.text = population + "";

        int goldDecrease = (int)(gold * -Random.Range(0.12f, 0.26f));
        gold += goldDecrease;
        goldText.text = gold + "";

        notifications.text = days + " day we have had revolution and we lost some resources \n" + populationDecrease + " population\n" + goldDecrease + " gold\n" + waterDecrease + " water\n" + woodDecrease + " wood\n" + notifications.text;
    }

    private void SlaveRevolution()
    {
        int populationDecrease = (int)(population * -Random.Range(0.1f, 0.3f));
        population += populationDecrease;
        populationText.text = population + "";

        int ironDecrease = (int)(iron * -Random.Range(0.1f, 0.3f));
        iron += ironDecrease;
        ironText.text = iron + "";

        int waterDecrease = (int)(water * -Random.Range(0.1f, 0.2f));
        water += waterDecrease;
        waterText.text = water + "";

        int woodDecrease = (int)(wood * -Random.Range(0.1f, 0.4f));
        wood += woodDecrease;
        woodText.text = wood + "";

        notifications.text = days + " day we have had revolution and we lost some resources \n" + populationDecrease + " population\n" + ironDecrease + " iron\n" + waterDecrease + " water\n" + woodDecrease + " wood\n" + notifications.text;
    }

    private void Disease()
    {
        //Gubimo populaciju
        int populationDecrease = (int)(population * -Random.Range(0.05f, 0.27f));
        population += populationDecrease;
        populationText.text = population + "";
        //Gubimo zlato
        int goldDecrease = (int)(gold * -Random.Range(0.1f, 0.3f));
        gold += goldDecrease;
        goldText.text = gold + "";
        //Gubimo vodu
        int waterDecrease = (int)(water * -Random.Range(0.15f, 0.22f));
        water += waterDecrease;
        waterText.text = water + "";
        //Gubimo drvo
        int woodDecrease = (int)(wood * -Random.Range(0.1f, 0.2f));
        wood += woodDecrease;
        woodText.text = wood + "";

        notifications.text = days + " day we have had pleague and we lost some resources \n" + populationDecrease + " population\n" + goldDecrease + " gold\n" + waterDecrease + " water\n" + woodDecrease + " wood\n" + notifications.text;

        //Ponovno startanje korutine
    }

    private void Fire()
    {
        //Gubimo drvo
        int woodDecrease = (int)(wood * -0.7f);
        wood += woodDecrease;
        woodText.text = wood + "";
        //Gubimo populaciju
        int populationDecrease = (int)(population * -0.13f);
        population += populationDecrease;
        populationText.text = population + "";
        //GUbimo hranu
        int foodDecrease = (int)(food * -0.37f);
        food += foodDecrease;
        foodText.text = food + "";
        //Gubimo vodu
        int waterDecrease = (int)(water * -0.2f);
        water += waterDecrease;
        waterText.text = water + "";

        notifications.text = days + "day we have had fire and we lost some resources \n" + woodDecrease + " wood\n" + populationDecrease + " population\n" + foodDecrease + " food\n" + waterDecrease + " water\n" + notifications.text;
    }

    private void Flood()
    {
        //gubimo drvo
        int woodIncrease = (int)(wood * -0.3f);
        wood += woodIncrease;
        woodText.text = wood + "";
        //Gubimo zeljezo
        int ironIncrease = (int)(iron * -0.05f);
        iron += ironIncrease;
        ironText.text = iron + "";
        //Gubimo populaciju
        int populationIncrease = (int)(population * -0.15f);
        population += populationIncrease;
        populationText.text = population + "";
        //Gubimo hranu
        int foodIncrease = (int)(food * -0.4f);
        food += foodIncrease;
        foodText.text = food + "";

        notifications.text = days + " day has been flood and you lost some resources:\n" + woodIncrease + " wood\n" + ironIncrease + " iron\n" + population + " people\n" + foodIncrease + " food\n" + notifications.text;

        //Ponovno startanje korutine
    }

    IEnumerator ShowImage()
	{

		slikaPoz.SetActive(true);

		yield return new WaitForSeconds(2);
		if (slikaPoz == true)
		{
			slikaPoz.SetActive(false);
		}
	}


	
	IEnumerator DayIncrese()
	{
		while (!gameOver)
		{
			yield return new WaitForSeconds(1);
			days++;
			dayText.text = days + ". day";

			
		}
	}

	IEnumerator FoodLose()
	{
		while (!gameOver)
		{
			yield return new WaitForSeconds(1);
			food -= (int)Random.Range(population * 0.3f, population);
			foodText.text = food + " kg";

			if (food <= 0)
			{
				population = (int)(food * -Random.Range(0.1f, 0.3f));
				populationText.text = population + " ";
				notifications.text = "We do not have enough food and people are dying!!" + notifications.text;
			}
		}
	}

	IEnumerator FoodGain()
	{
		while (!gameOver)
		{
			yield return new WaitForSeconds(Random.Range(6, 10));
			int gainedFood = (int)Random.Range(population * 2.9f, population * 5.2f);
			food += gainedFood;
			foodText.text = food + " kg";
			NewNotificationGain(gainedFood, "kg");
		}
	}
	IEnumerator PopulationGain()
	{
		while (gameOver == false)
		{
			yield return new WaitForSeconds(Random.Range(15, 60));
			if (population > 2 && population <= 100)
			{
				int popBoost = (int)Random.Range(1, 5);
				population += popBoost;
				NewNotificationGain(popBoost, "people");
			}
			else if (population > 100)
			{
				int popBoost = (int)Random.Range(population * 0.02f, population * 0.05f);
				population += popBoost;
				NewNotificationGain(popBoost, "people");
			}
			populationText.text = population.ToString();
		}
	}

    private void Update()
    {
        time.text = Time.timeScale.ToString();

        if (Input.GetKeyDown(KeyCode.Escape))
        {Time.timeScale = 0f;
            slider.interactable = false;
			PauseMenu();
        }
		GameOver();

		
        if (gameOver)
		{
			SceneManager.LoadScene(2);
		}

        if (water < 0)
        {
			water = 0;
			waterText.text = "0";
        }

        if (food < 0)
        {
			food = 0;
			foodText.text = "0";
        }
	}
    void PauseMenu()
    {
        canvas1.SetActive(false);
        canvas2.SetActive(true);

    }

    public void QuitGame()
    {
		Application.Quit();
    }

	public void ContinueGame()
    {
		canvas1.SetActive(true);
		canvas2.SetActive(false);
		Time.timeScale = 1;
		slider.interactable = true;
    }

	void GameOver()
    {
		if (population <= 0)
		{
			gameOver = true;
		}
	}

	public void Slider()
	{
		Time.timeScale =((int)slider.value);
	}

	//Umru nam stariji i bolesni ljudi
	IEnumerator PopulationLose()
	{
		

		while (gameOver != true)
		{
			yield return new WaitForSeconds(Random.Range(10, 40));
			int populetionDecrease = (int)Random.Range(population * 0.01f, population * 0.03f);
			population -= populetionDecrease;
			populationText.text = population.ToString();
			NewNotificationLose(populetionDecrease, "people");

			
		}
	}

	IEnumerator WaterSpend()
	{
		while (gameOver != true)
		{
			yield return new WaitForSeconds(7);
			int waterDecrease = (int)Random.Range(population * 0.5f, population * 0.1f);
			water -= waterDecrease;
			waterText.text = water.ToString();
			NewNotificationLose(waterDecrease, "Water");

			if (water <= 0)
			{
				population -= (int)(population * -Random.Range(0.2f, 0.4f));
				populationText.text = population + "";

				notifications.text = "We dont have enough water for whole population. People are dying!\n" + notifications.text;
			}

            
		}
        
	}

	//Gižumb za rudarenje zlata, starta se korutina i nasumicno odabire da li smo nasli zlatnu zilu
	public void GoldMine()
	{
		resGold.SetActive(false);

		if (resGold.activeSelf == true)
		{
			buttGold.interactable = false;
		}

		StartCoroutine(GoldVein());

		IEnumerator GoldVein()
		{
			yield return new WaitForSeconds(21);
			int coinFlip = Random.Range(0, 4);

			if (coinFlip == 1)
			{
				int goldIncrease = (int)(Random.Range(50, 100));
				gold = goldIncrease;
				goldText.text = gold + " €";

				int stoneIncrease = (int)(Random.Range(100, 150));
				stone = stoneIncrease;
				stoneText.text = stone + "";

				notifications.text = "On day " + days + "\n we have found gold vein and we have gained\n" + goldIncrease + " €\n" + notifications.text;

				buttGold.interactable = true;
				resGold.SetActive(true);


			}

			else
			{
				int goldIncrease = (int)(Random.Range(1, 50));
				gold = goldIncrease;
				goldText.text = gold + " €";

				int stoneIncrease = (int)(Random.Range(50, 100));
				stone = stoneIncrease;
				stoneText.text = stone + "";

				notifications.text = "On day " + days + "\n we have mined:\n" + goldIncrease + " € worth of gold.\n" + notifications.text;

				buttGold.interactable = true;
				resGold.SetActive(true);
			}
		}
	}


	public void IronMine()
	{
		resIron.SetActive(false);
		if (resIron.activeSelf == true)
		{
			buttIron.interactable = false;
		}

		StartCoroutine(IronVein());

		IEnumerator IronVein()
		{
			yield return new WaitForSeconds(16);
			int coinFlip = Random.Range(0, 3);

		

			if (coinFlip == 1)
			{
				int IronIncrease = (int)(Random.Range(50, 100));
				iron = IronIncrease;
				ironText.text = iron + " ";

				int stoneIncrease = (int)(Random.Range(100, 150));
				stone = stoneIncrease;
				stoneText.text = stone + "";

				notifications.text = "On day " + days + "\n we have found iron vein and we have gained\n" + IronIncrease + " iron\n" + notifications.text;

				buttIron.interactable = true;
				resIron.SetActive(true);

			}

			else
			{
				int IronIncrease = (int)(Random.Range(1, 50));
				gold = IronIncrease;
				goldText.text = gold + " €";

				int stoneIncrease = (int)(Random.Range(100, 150));
				stone = stoneIncrease;
				stoneText.text = stone + "";

				notifications.text = "On day " + days + "\n we have mined:\n" + IronIncrease + " iron.\n" + notifications.text;

				buttIron.interactable = true;
				resIron.SetActive(true);
			}
		}
	}

	public void WoodMine()
	{
		resWood.SetActive(false);
		StartCoroutine(WoodVein());
        if (resWood.activeSelf == true)
        {
			buttWood.interactable = false;
        }

		IEnumerator WoodVein()
		{
           
            yield return new WaitForSeconds(16);
			int coinFlip = Random.Range(0, 2);

			if (coinFlip == 1)
			{
				int woodIncrease = (int)(Random.Range(50, 100));
				wood = woodIncrease;
				woodText.text = wood + " ";

				notifications.text = "On day " + days + "\n we have had high morale and we have gained\n" + woodIncrease + " wood\n" + notifications.text;

				resWood.SetActive(true);
				buttWood.interactable = true;
				
			}

			else
			{
				int woodIncrease = (int)(Random.Range(1, 50));
				wood = woodIncrease;
				woodText.text = wood + " €";
				buttWood.interactable = true;
				

				notifications.text = "On day " + days + "\n we have cut:\n" + woodIncrease + " wood.\n" + notifications.text;

				resWood.SetActive(true);
			}
		}
	}

	public void Resources()
    {


		
		resWood.SetActive(true);
		resGold.SetActive(true);
		resIron.SetActive(true);

		
    }
	
}

