using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TopController : MonoBehaviour
{
    public Button btn;
    public Text zaman, can , durum;
    private Rigidbody rg;
    public float Hiz = 1.5f;
    float zamanSayaci = 15;
    int canSayac = 3;
    bool oyunDevam = true;
    bool oyunTamam = false;
    // Start is called before the first frame update
    void Start()
    {
        rg = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (oyunDevam && !oyunTamam)
        {
            zamanSayaci -= Time.deltaTime;
            zaman.text = (int)zamanSayaci + "";
        }
        else if(!oyunTamam)
        {
            durum.text = "Oyun Tamamlanamadı.";
            btn.gameObject.SetActive(true);
        }
        if (zamanSayaci < 0) 
            oyunDevam = false;
    }
    private void FixedUpdate()
    {
        if (oyunDevam && !oyunTamam)
        {
            float yatay = Input.GetAxis("Horizontal");
            float dikey = Input.GetAxis("Vertical");
            Vector3 kuvvet = new Vector3(-dikey, 0, yatay);
            rg.AddForce(kuvvet * Hiz);
        }
        else
        {
            rg.velocity = Vector3.zero;
            rg.angularVelocity = Vector3.zero;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        string objİsmi = collision.gameObject.name;
        if (objİsmi.Equals("Bitis"))
        {
            //print("Oyun Tamalandı");
            oyunTamam = true;
            durum.text = "Oyun Tamamlandı. Tebrikler!";
            btn.gameObject.SetActive(true);

        }
        else if(!objİsmi.Equals("LabZemin")&& !objİsmi.Equals("Zemin"))
        {
            canSayac -= 1;
            can.text = canSayac + "";
            if (canSayac == 0)
                oyunDevam = false;
        }
    }
}
