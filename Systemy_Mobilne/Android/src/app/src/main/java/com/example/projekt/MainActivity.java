package com.example.projekt;

import android.content.Context;
import android.content.Intent;
import android.hardware.Sensor;
import android.hardware.SensorEvent;
import android.hardware.SensorEventListener;
import android.hardware.SensorManager;
import android.os.Bundle;
import android.util.Log;
import android.view.View;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.EditText;
import android.widget.Spinner;
import android.widget.TextView;

import androidx.appcompat.app.AppCompatActivity;

import java.util.ArrayList;
import java.util.List;

import retrofit2.Retrofit;
import retrofit2.converter.gson.GsonConverterFactory;
import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;

public class MainActivity extends AppCompatActivity implements SensorEventListener {
    private SensorManager sensorManager;
    private Sensor accelerometer;
    private EditText amountField;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        sensorManager = (SensorManager) getSystemService(Context.SENSOR_SERVICE);
        accelerometer = sensorManager.getDefaultSensor(Sensor.TYPE_ACCELEROMETER);
        sensorManager.registerListener(this, accelerometer, SensorManager.SENSOR_DELAY_NORMAL);

        setContentView(R.layout.activity_main);

        Spinner currencyFrom = findViewById(R.id.currencyFrom);
        Spinner currencyTo = findViewById(R.id.currencyTo);
        amountField = findViewById(R.id.amountField);

        Button showCapitalsButton = findViewById(R.id.showCapitalsButton);
        showCapitalsButton.setOnClickListener(v -> {
            Intent intent = new Intent(MainActivity.this, CapitalActivity.class);
            startActivity(intent);
        });

        Button showCurrencyRatesButton = findViewById(R.id.showCurrencyRatesButton);
        showCurrencyRatesButton.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                Intent intent = new Intent(MainActivity.this, CurrencyRatesActivity.class);
                startActivity(intent);
            }
        });


        Button convertButton = findViewById(R.id.convertButton);
        TextView resultField = findViewById(R.id.resultField);

        // Pusta lista walut
        List<String> currencyList = new ArrayList<>();

        // Tworzymy adapter
        ArrayAdapter<String> currencyAdapter = new ArrayAdapter<>(this, android.R.layout.simple_spinner_item, currencyList);
        currencyAdapter.setDropDownViewResource(android.R.layout.simple_spinner_dropdown_item);

        //Adapter na Spinnerach
        currencyFrom.setAdapter(currencyAdapter);
        currencyTo.setAdapter(currencyAdapter);

        // Tworzenie Retrofita
        Retrofit retrofit = new Retrofit.Builder()
                .baseUrl("https://api.nbp.pl/api/exchangerates/")
                .addConverterFactory(GsonConverterFactory.create())
                .build();

        // Inicjalizacja serwisu
        CurrencyService service = retrofit.create(CurrencyService.class);

        // Wywołanie API
        service.getCurrencyTable().enqueue(new Callback<List<CurrencyTable>>() {
            @Override
            public void onResponse(Call<List<CurrencyTable>> call, Response<List<CurrencyTable>> response) {
                if (response.isSuccessful() && response.body() != null) {
                    // Lista zawierająca tylko kody walut
                    List<String> newCurrencyList = new ArrayList<>();
                    for (Currency currency : response.body().get(0).getRates()) {
                        newCurrencyList.add(currency.getCode()); // Dodawanie kodu waluty
                    }

                    // Ustawianie adapterów dla spinnerów
                    ArrayAdapter<String> newCurrencyAdapter = new ArrayAdapter<>(MainActivity.this, android.R.layout.simple_spinner_item, newCurrencyList);
                    newCurrencyAdapter.setDropDownViewResource(android.R.layout.simple_spinner_dropdown_item);
                    currencyFrom.setAdapter(newCurrencyAdapter);
                    currencyTo.setAdapter(newCurrencyAdapter);
                }
            }


            @Override
            public void onFailure(Call<List<CurrencyTable>> call, Throwable t) {
                Log.e("API_ERROR", t.getMessage());
            }
        });
        convertButton.setOnClickListener(v -> {
            // Wybrane waluty
            String fromCurrency = currencyFrom.getSelectedItem().toString();
            String toCurrency = currencyTo.getSelectedItem().toString();

            // Kwota z pola
            String amountStr = amountField.getText().toString();
            if (amountStr.isEmpty()) {
                resultField.setText("Please enter an amount.");
                return;
            }

            double amount = Double.parseDouble(amountStr);

            service.getCurrencyRates(fromCurrency).enqueue(new Callback<CurrencyTable>() {
                @Override
                public void onResponse(Call<CurrencyTable> call, Response<CurrencyTable> response) {
                    if (response.isSuccessful() && response.body() != null) {
                        double fromCurrencyRate = response.body().getRates().get(0).getMid();
                        double amountInPLN = amount * fromCurrencyRate;

                        service.getCurrencyRates(toCurrency).enqueue(new Callback<CurrencyTable>() {
                            @Override
                            public void onResponse(Call<CurrencyTable> call, Response<CurrencyTable> response) {
                                if (response.isSuccessful() && response.body() != null) {
                                    double toCurrencyRate = response.body().getRates().get(0).getMid();
                                    double convertedAmount = amountInPLN / toCurrencyRate;

                                    // Wyświetlenie wyniku
                                    resultField.setText(String.format("%.2f %s = %.2f %s", amount, fromCurrency, convertedAmount, toCurrency));
                                }
                            }

                            @Override
                            public void onFailure(Call<CurrencyTable> call, Throwable t) {
                                Log.e("API_ERROR", t.getMessage());
                                resultField.setText("Error occurred while fetching rates for the target currency.");
                            }
                        });
                    }
                }

                @Override
                public void onFailure(Call<CurrencyTable> call, Throwable t) {
                    Log.e("API_ERROR", t.getMessage());
                    resultField.setText("Error occurred while fetching rates for the source currency.");
                }
            });
        });

    }
    @Override
    public void onSensorChanged(SensorEvent event) {
        float x = event.values[0];
        float y = event.values[1];
        float z = event.values[2];

        if (Math.sqrt(x * x + y * y + z * z) > 12) {
            amountField.setText("");

            Spinner currencyFrom = findViewById(R.id.currencyFrom);
            Spinner currencyTo = findViewById(R.id.currencyTo);

            currencyFrom.setSelection(0);
            currencyTo.setSelection(0);

        }
    }
    @Override
    public void onAccuracyChanged(Sensor sensor, int accuracy) {}
}
