using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetManager : Singleton<PetManager>
{
    public Transform[] petStartTransform = new Transform[2];
    public Transform[] playerByPetPos = new Transform[2];
    private PetInventory petInventory;
    private GameObject[] petGameObject = new GameObject[2];
    private List<GameObject> petObjectList = new List<GameObject>();
    private void Start()
    {
        if (petInventory == null)
        {
            petInventory = InventorySystem.Instance.petInventory;
            //var pets = petInventory.equipPets;
            //for (int i = 0; i < pets.Length; i++)
            //{
            //    if (pets[i] !=null)
            //    {
            //        var make = Instantiate(petInventory.equipPets[i].pet.Pet_GameObjet);
            //        make.transform.position = petStartTransform[i].position;
            //        make.GetComponent<PetController>().lerpPos = playerByPetPos[i];
            //        petGameObject[i] =make;
            //    }
            //}
        }
    }

    private void Update()
    {
        //if(Input.GetKeyDown(KeyCode.Return)) 
        //{
        //    petGameObject[0].GetComponent<PetController>().SetState(States.Idle);
        //    petGameObject[0].SetActive(false);
        //    petObjectList.Add(petGameObject[0]);
        //    var make = Instantiate(petInventory.pets[1].pet.Pet_GameObjet);
        //    make.transform.position = playerByPetPos[0].transform.position;
        //    make.GetComponent<PetController>().lerpPos = playerByPetPos[0];
        //    petGameObject[0] = make;
        //}

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            DequipPets(0);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            DequipPets(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            ChagngePet(0, petInventory.pets[0]);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            ChagngePet(1, petInventory.pets[1]);
        }
    }
    public void ChagngePet(int index, PetInventory.InventoryPet pet)
    {
        switch (index)
        {
            case 0:
                if (petGameObject[0] == null)
                {
                    var check = CheckPetList(pet.pet.Pet_GameObjet);
                    if (check != null)
                    {
                        check.transform.position = playerByPetPos[0].transform.position;
                        check.GetComponent<PetController>().lerpPos = playerByPetPos[0];
                        check.GetComponent<PetController>().SetState(States.Idle);
                        check.GetComponent<PetController>().index = 0;
                        check.SetActive(true);
                        petGameObject[0] = check;
                    }
                    else
                    {
                        var make = Instantiate(pet.pet.Pet_GameObjet);
                        make.transform.position = playerByPetPos[0].transform.position;
                        make.GetComponent<PetController>().lerpPos = playerByPetPos[0];
                        if (petObjectList.Count > 0)
                            make.GetComponent<PetController>().isArrive = true;
                        make.GetComponent<PetController>().index = 0;
                        petGameObject[0] = make;
                    }
                }
                else
                {
                    petGameObject[0].GetComponent<PetController>().SetState(States.Idle);
                    petGameObject[0].SetActive(false);
                    AddPetList(petGameObject[0]);
                    petGameObject[0] = null;

                    var check = CheckPetList(pet.pet.Pet_GameObjet);
                    if (check != null)
                    {
                        check.transform.position = playerByPetPos[0].transform.position;
                        check.GetComponent<PetController>().lerpPos = playerByPetPos[0];
                        check.GetComponent<PetController>().SetState(States.Idle);
                        check.SetActive(true);
                        check.GetComponent<PetController>().index = 0;
                        petGameObject[0] = check;
                    }
                    else
                    {
                        var make = Instantiate(pet.pet.Pet_GameObjet);
                        make.transform.position = playerByPetPos[0].transform.position;
                        make.GetComponent<PetController>().lerpPos = playerByPetPos[0];
                        if (petObjectList.Count > 0)
                            make.GetComponent<PetController>().isArrive = true;
                        make.GetComponent<PetController>().index = 0;
                        petGameObject[0] = make;
                    }
                }
                break;
            case 1:
                if (petGameObject[1] == null)
                {
                    var check = CheckPetList(pet.pet.Pet_GameObjet);
                    if (check != null)
                    {
                        check.transform.position = playerByPetPos[1].transform.position;
                        check.GetComponent<PetController>().lerpPos = playerByPetPos[1];
                        check.GetComponent<PetController>().SetState(States.Idle);
                        check.GetComponent<PetController>().index = 1;
                        check.SetActive(true);

                        petGameObject[1] = check;
                    }
                    else
                    {
                        var make = Instantiate(pet.pet.Pet_GameObjet);
                        make.transform.position = playerByPetPos[1].transform.position;
                        make.GetComponent<PetController>().lerpPos = playerByPetPos[1];
                        if (petObjectList.Count > 0)
                            make.GetComponent<PetController>().isArrive = true;
                        make.GetComponent<PetController>().index = 1;
                        petGameObject[1] = make;
                    }
                }
                else
                {
                    petGameObject[1].GetComponent<PetController>().SetState(States.Idle);
                    petGameObject[1].SetActive(false);
                    AddPetList(petGameObject[1]);
                    petGameObject[1] = null;

                    var check = CheckPetList(pet.pet.Pet_GameObjet);
                    if (check != null)
                    {
                        check.transform.position = playerByPetPos[1].transform.position;
                        check.GetComponent<PetController>().lerpPos = playerByPetPos[1];
                        check.GetComponent<PetController>().SetState(States.Idle);
                        check.GetComponent<PetController>().index = 1;
                        check.SetActive(true);
                        petGameObject[1] = check;
                    }
                    else
                    {
                        var make = Instantiate(pet.pet.Pet_GameObjet);
                        make.transform.position = playerByPetPos[1].transform.position;
                        make.GetComponent<PetController>().lerpPos = playerByPetPos[1];
                        if (petObjectList.Count > 0)
                            make.GetComponent<PetController>().isArrive = true;
                        make.GetComponent<PetController>().index = 1;
                        petGameObject[1] = make;
                    }
                }
                break;
        }
    }
    public void DequipPets(int index)
    {
        switch (index)
        {
            case 0:
                petGameObject[0].GetComponent<PetController>().GetPetAnimator().Rebind();
                petGameObject[0].GetComponent<PetController>().SetState(States.Idle);
                petGameObject[0].SetActive(false);
                AddPetList(petGameObject[0]);
                petGameObject[0] = null;
                break;
            case 1:
                petGameObject[1].GetComponent<PetController>().GetPetAnimator().Rebind();
                petGameObject[1].GetComponent<PetController>().SetState(States.Idle);
                petGameObject[1].SetActive(false);
                AddPetList(petGameObject[1]);
                petGameObject[1] = null;
                break;
        }
    }
    private void AddPetList(GameObject obj)
    {
        foreach (var pet in petObjectList)
        {
            if (pet.GetComponent<PetController>().petName == obj.name)
            {
                return;
            }
        }
        petObjectList.Add(obj);
    }
    private GameObject CheckPetList(GameObject obj)
    {
        foreach (var pet in petObjectList)
        {
            if (pet.GetComponent<PetController>().petName == obj.name)
            {
                return pet;
            }
        }
        return null;
    }
    public GameObject[] GetPetGameobjectArray()
    {
        return petGameObject;
    }
        
}
