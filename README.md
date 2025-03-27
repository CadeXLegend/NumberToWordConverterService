# Number To Word Converter Service

This service takes in a number and converts it into words!

It takes in an unsigned integer (0-4294967295) currently

The point of this specific project is to achieve it using a Graph-based apporoach

## TODO

- Unit Tests
- Endpoint Tests
- ~~Fix a bug where And is sometimes missing~~
- ~~Refactor to remove .Trim() from String as it is an unnecessary call due to missing logic~~
- Create a Front-End Web UI for this
- Extend to work with Decimals/Floats
- Extend to add optional end-of-word notations
     - Such as: "Dollars", "Pents", "Rubles", "Sheckles", and so on
- Add optional comma delimiters for the number's triadic groupings
- Full CI/CD Suite
- Enhanced Logging for Unavoidable System Exceptions
     - When a Result itself can't be caught due to an inbuilt Exception
          - One example being: Providing a string instead of a number in the API request
- ~~Minor Refactoring~~

## Running It Locally

1. Have Dotnet 8 Installed
2. Open a Terminal in this Directory
3. Run the program 

     ```
     dotnet run
     ```

## Local Usage Examples

### Short Number

#### Request

```bash
curl -X POST http://localhost:5021/number-to-words \
     -H "Content-Type: application/json" \
     -d '{"number": 211}'
```

#### Response

```
Two Hundred And Eleven
```

### Medium Number

#### Request

```bash
curl -X POST http://localhost:5021/number-to-words \
     -H "Content-Type: application/json" \
     -d '{"number": 420069}'
```

#### Response
```
Four Hundred And Twenty Thousand And Sixty-Nine
```

### Another Medium Number

#### Request

```bash
curl -X POST http://localhost:5021/number-to-words \
     -H "Content-Type: application/json" \
     -d '{"number": 420169}'
```

#### Response
```
Four Hundred And Twenty Thousand One Hundred And Sixty-Nine
```

### Large Number

#### Request

```bash
curl -X POST http://localhost:5021/number-to-words \
     -H "Content-Type: application/json" \
     -d '{"number": 222222222}'
```

#### Response

```
Two Hundred And Twenty-Two Million Two Hundred And Twenty-Two Thousand Two Hundred And Twenty-Two
```

### Another Large Number

#### Request

```bash
curl -X POST http://localhost:5021/number-to-words \
     -H "Content-Type: application/json" \
     -d '{"number": 1200056}'
```

#### Response

```
One Million Two Hundred Thousand And Fifty-Six
```
