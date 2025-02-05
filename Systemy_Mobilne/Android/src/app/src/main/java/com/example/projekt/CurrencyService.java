package com.example.projekt;

import java.util.List;

import retrofit2.Call;
import retrofit2.http.GET;
import retrofit2.http.Path;

public interface CurrencyService {
    @GET("tables/A/")
    Call<List<CurrencyTable>> getCurrencyTable();

    @GET("rates/a/{currency}/")
    Call<CurrencyTable> getCurrencyRates(@Path("currency") String currency);
}
