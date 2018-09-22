# PrettierTestLogger

Prettier formatting for your dotnet core tests. Tested with xunit.

[![Build Status](https://travis-ci.org/saintedlama/prettiertestlogger.svg?branch=master)](https://travis-ci.org/saintedlama/prettiertestlogger)
[![Coverage Status](https://coveralls.io/repos/github/saintedlama/prettiertestlogger/badge.svg?branch=master)](https://coveralls.io/github/saintedlama/prettiertestlogger?branch=master)

## Motivation

Get output like this

![With PrettierTestLogger](https://raw.githubusercontent.com/saintedlama/prettiertestlogger/master/assets/prettier_test_logger.png)

instead of this

![Without PrettierTestLogger](https://raw.githubusercontent.com/saintedlama/prettiertestlogger/master/assets/no_prettier_test_logger.png)

## Installation

```bash
dotnet add package PrettierTestLogger
```

## Usage

```bash
dotnet test --logger prettier
```
