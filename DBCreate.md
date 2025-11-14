Here is the **Markdown (.md) version** of the full PostgreSQL 17 restore guide with all commands clearly formatted.

---

# 🐘 PostgreSQL 17 — Database Create & Restore Guide (Port 5433)

Your PostgreSQL 17 server is running on **port 5433**, so database creation and restore must happen on that port.
Use this guide to properly create and restore your `smartledger` database.

---

## ✅ **STEP 1 — Create the database on port 5433**

```bash
createdb -U postgres -p 5433 smartledger
```

### ✔ Verify that DB exists

```bash
psql -U postgres -p 5433 -l
```

You should now see `smartledger` in the list.

---

## ✅ **STEP 2 — Restore the custom-format dump using pg_restore**

Since your dump is **custom-format**, you must use `pg_restore` (NOT `psql`).

### Basic restore command:

```bash
pg_restore -U postgres -p 5433 -d smartledger "D:\Freelancing project\SmartLedger\SmartLedger\SmartLedger.sql"
```

### Optional: verbose mode

```bash
pg_restore -U postgres -p 5433 -d smartledger --verbose "D:\Freelancing project\SmartLedger\SmartLedger\SmartLedger.sql"
```

---

## ⚠️ **If pgAdmin is using the wrong pg_restore (from PG16)**

Then use the full PostgreSQL 17 binary path:

```bash
"C:\Program Files\PostgreSQL\17\bin\pg_restore.exe" ^
  -U postgres ^
  -p 5433 ^
  -d smartledger ^
  --verbose ^
  "D:\Freelancing project\SmartLedger\SmartLedger\SmartLedger.sql"
```

---

## 🎉 **You Are Done!**

### Why this works:

* ✔ PostgreSQL 17 server is on **port 5433**
* ✔ `smartledger` database created on **5433**
* ✔ `pg_restore` also runs on **5433**
* ✔ Custom-format dump restored correctly

No more port mismatch errors. No more `3D000: database does not exist` issues.

---

