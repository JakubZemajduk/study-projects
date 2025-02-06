package com.example.projekt;

import android.os.Bundle;
import android.util.Log;
import android.view.View;
import android.widget.AdapterView;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.Spinner;

import androidx.activity.EdgeToEdge;
import androidx.appcompat.app.AppCompatActivity;
import androidx.core.graphics.Insets;
import androidx.core.view.ViewCompat;
import androidx.core.view.WindowInsetsCompat;

import com.google.android.gms.maps.CameraUpdateFactory;
import com.google.android.gms.maps.SupportMapFragment;
import com.google.android.gms.maps.model.LatLng;
import com.google.android.gms.maps.model.MarkerOptions;

import java.util.ArrayList;
import java.util.List;

import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;
import retrofit2.Retrofit;
import retrofit2.converter.gson.GsonConverterFactory;

public class CapitalActivity extends AppCompatActivity {

    private SupportMapFragment mapFragment;
    private Spinner currencySpinner;
    private Button backButton;
    private CurrencyService service;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_capital);

        currencySpinner = findViewById(R.id.currencySpinner);
        backButton = findViewById(R.id.backButton);
        mapFragment = (SupportMapFragment) getSupportFragmentManager().findFragmentById(R.id.map);

        // Retrofit i API
        Retrofit retrofit = new Retrofit.Builder()
                .baseUrl("https://api.nbp.pl/api/exchangerates/")
                .addConverterFactory(GsonConverterFactory.create())
                .build();
        service = retrofit.create(CurrencyService.class);

        // Pobieranie walut z API
        fetchCurrencies();

        currencySpinner.setOnItemSelectedListener(new AdapterView.OnItemSelectedListener() {
            @Override
            public void onItemSelected(AdapterView<?> parent, View view, int position, long id) {
                String selectedCurrency = currencySpinner.getSelectedItem().toString();
                LatLng capitalLocation = getCapitalLocation(selectedCurrency);
                updateMap(capitalLocation, "Capital of " + selectedCurrency);
            }

            @Override
            public void onNothingSelected(AdapterView<?> parent) {}
        });

        // Obsługa przycisku powrotu
        backButton.setOnClickListener(v -> finish());
    }

    private void fetchCurrencies() {
        service.getCurrencyTable().enqueue(new Callback<List<CurrencyTable>>() {
            @Override
            public void onResponse(Call<List<CurrencyTable>> call, Response<List<CurrencyTable>> response) {
                if (response.isSuccessful()) {
                    List<String> currencies = new ArrayList<>();
                    for (Currency currency : response.body().get(0).getRates()) {
                        currencies.add(currency.getCode());
                    }

                    ArrayAdapter<String> adapter = new ArrayAdapter<>(CapitalActivity.this,
                            android.R.layout.simple_spinner_item, currencies);
                    adapter.setDropDownViewResource(android.R.layout.simple_spinner_dropdown_item);
                    currencySpinner.setAdapter(adapter);
                }
            }

            @Override
            public void onFailure(Call<List<CurrencyTable>> call, Throwable t) {
                Log.e("API_ERROR", t.getMessage());
            }
        });
    }

    private LatLng getCapitalLocation(String currency) {
        switch (currency) {
            case "THB": return new LatLng(13.7563, 100.5018); // Bangkok, Tajlandia
            case "USD": return new LatLng(38.8954, -77.0365); // Waszyngton, USA
            case "AUD": return new LatLng(-35.2809, 149.1300); // Canberra, Australia
            case "HKD": return new LatLng(22.3193, 114.1694); // Hongkong
            case "CAD": return new LatLng(45.4215, -75.6972); // Ottawa, Kanada
            case "NZD": return new LatLng(-41.2867, 174.7762); // Wellington, Nowa Zelandia
            case "SGD": return new LatLng(1.3521, 103.8198); // Singapur
            case "EUR": return new LatLng(50.8503, 4.3517); // Bruksela
            case "HUF": return new LatLng(47.4979, 19.0402); // Budapeszt, Węgry
            case "CHF": return new LatLng(46.9481, 7.4474); // Berno, Szwajcaria
            case "GBP": return new LatLng(51.5074, -0.1278); // Londyn, Wielka Brytania
            case "UAH": return new LatLng(50.4501, 30.5036); // Kijów, Ukraina
            case "JPY": return new LatLng(35.6762, 139.6503); // Tokio, Japonia
            case "CZK": return new LatLng(50.0755, 14.4378); // Praga, Czechy
            case "DKK": return new LatLng(55.6761, 12.5683); // Kopenhaga, Dania
            case "ISK": return new LatLng(64.1355, -21.8954); // Reykjavik, Islandia
            case "NOK": return new LatLng(59.9139, 10.7522); // Oslo, Norwegia
            case "SEK": return new LatLng(59.3293, 18.0686); // Sztokholm, Szwecja
            case "HRK": return new LatLng(45.8131, 15.978); // Zagrzeb, Chorwacja
            case "RON": return new LatLng(44.4268, 26.1025); // Bukareszt, Rumunia
            case "BGN": return new LatLng(42.6975, 23.3242); // Sofia, Bułgaria
            case "TRY": return new LatLng(39.9334, 32.8597); // Ankara, Turcja
            case "ILS": return new LatLng(31.7683, 35.2137); // Jerozolima, Izrael
            case "CLP": return new LatLng(-33.4489, -70.6693); // Santiago, Chile
            case "PHP": return new LatLng(14.5995, 120.9842); // Manila, Filipiny
            case "MXN": return new LatLng(19.4326, -99.1332); // Meksyk, Meksyk
            case "ZAR": return new LatLng(-25.7460, 28.1871); // Pretoria, RPA
            case "BRL": return new LatLng(-15.7801, -47.9292); // Brasília, Brazylia
            case "MYR": return new LatLng(3.1390, 101.6869); // Kuala Lumpur, Malezja
            case "IDR": return new LatLng(-6.2088, 106.8456); // Dżakarta, Indonezja
            case "INR": return new LatLng(28.6139, 77.2090); // Nowe Delhi, Indie
            case "KRW": return new LatLng(37.5665, 126.9780); // Seul, Korea Południowa
            case "CNY": return new LatLng(39.9042, 116.4074); // Pekin, Chiny
            case "XDR": return new LatLng(38.8954, -77.0365); // Waszyngton,

            default: return new LatLng(0, 0);
        }
    }

    private void updateMap(LatLng location, String title) {
        mapFragment.getMapAsync(googleMap -> {
            googleMap.clear();
            googleMap.addMarker(new MarkerOptions().position(location).title(title));
            googleMap.moveCamera(CameraUpdateFactory.newLatLngZoom(location, 10));
        });
    }
}
