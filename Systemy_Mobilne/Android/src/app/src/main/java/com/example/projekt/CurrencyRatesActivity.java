package com.example.projekt;

import android.os.Bundle;
import android.util.Log;
import android.view.View;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.ListView;
import android.widget.Toast;

import androidx.appcompat.app.AppCompatActivity;

import java.util.List;

import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;
import retrofit2.Retrofit;
import retrofit2.converter.gson.GsonConverterFactory;

public class CurrencyRatesActivity extends AppCompatActivity {

    private ListView currencyRatesList;
    private Button backButton;

    private static final String BASE_URL = "https://api.nbp.pl/api/exchangerates/";

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_currency_rates);

        currencyRatesList = findViewById(R.id.currencyRatesList);
        backButton = findViewById(R.id.backButton);

        backButton.setOnClickListener(v -> finish());

        // Wczytywanie danych o walutach
        loadCurrencyRates();
    }

    private void loadCurrencyRates() {
        Retrofit retrofit = new Retrofit.Builder()
                .baseUrl(BASE_URL)
                .addConverterFactory(GsonConverterFactory.create())
                .build();

        CurrencyService currencyService = retrofit.create(CurrencyService.class);
        Call<List<CurrencyTable>> call = currencyService.getCurrencyTable();

        call.enqueue(new Callback<List<CurrencyTable>>() {
            @Override
            public void onResponse(Call<List<CurrencyTable>> call, Response<List<CurrencyTable>> response) {
                if (response.isSuccessful() && response.body() != null) {

                    List<Currency> currencies = response.body().get(0).getRates();

                    // Lista do wyświetlenia
                    String[] currencyInfo = new String[currencies.size()];
                    for (int i = 0; i < currencies.size(); i++) {
                        Currency currency = currencies.get(i);
                        currencyInfo[i] = currency.getCurrency() + " (" + currency.getCode() + "): " + currency.getMid();
                    }

                    // Wyświetlanie danych
                    ArrayAdapter<String> adapter = new ArrayAdapter<>(CurrencyRatesActivity.this, android.R.layout.simple_list_item_1, currencyInfo);
                    currencyRatesList.setAdapter(adapter);
                }
            }

            @Override
            public void onFailure(Call<List<CurrencyTable>> call, Throwable t) {
                Log.e("CurrencyRates", "Request failed", t);
                Toast.makeText(CurrencyRatesActivity.this, "Request failed", Toast.LENGTH_SHORT).show();
            }
        });
    }
}
