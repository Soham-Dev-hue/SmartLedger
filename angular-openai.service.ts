import { Injectable } from "@angular/core";
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Observable } from "rxjs";
import { environment } from "../environments/environment";

interface ChatRequest {
  prompt: string;
  systemMessage?: string;
}
interface ChatResponse {
  message: string /* add fields returned by API */;
}

@Injectable({ providedIn: "root" })
export class OpenAIService {
  private apiUrl = `${environment.apiUrl}/OpenAI`;

  constructor(private http: HttpClient) {}

  private authHeaders(): HttpHeaders {
    const token = localStorage.getItem("jwt"); // or your auth service
    return new HttpHeaders({
      "Content-Type": "application/json",
      ...(token ? { Authorization: `Bearer ${token}` } : {}),
    });
  }

  getChatCompletion(body: ChatRequest): Observable<ChatResponse> {
    return this.http.post<ChatResponse>(`${this.apiUrl}/chat`, body, {
      headers: this.authHeaders(),
    });
  }

  analyzeInvoice(invoiceData: string): Observable<any> {
    return this.http.post(
      `${this.apiUrl}/analyze-invoice`,
      { invoiceData },
      { headers: this.authHeaders() }
    );
  }

  predictCashflow(financialData: string): Observable<any> {
    return this.http.post(
      `${this.apiUrl}/predict-cashflow`,
      { financialData },
      { headers: this.authHeaders() }
    );
  }
}
